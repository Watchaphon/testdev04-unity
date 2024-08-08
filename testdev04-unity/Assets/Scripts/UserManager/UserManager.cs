using APIService.Server;
using Cysharp.Threading.Tasks;
using System;

public class UserManager
{
    private static UserManager m_instance;
    public static UserManager Instance
    {
        get
        {
            if(m_instance == null)
                m_instance = new UserManager();

            return m_instance;
        }
    }

    public int userId { get; private set; }
    public int userHeart { get; private set; }
    public int userDiamonds { get; private set; }

    /// <summary>
    /// ***In real case this should be on game config data.
    /// </summary>
    public const int MinHearts = 0;
    /// <summary>
    /// ***In real case this should be on game config data.
    /// </summary>
    public const int MaxHearts = 100;

    /// <summary>
    /// This invoke when user data updated.
    /// </summary>
    public event OnUserDataUpdate onServerDataUpdate;

    public void SetUserId(int userId)
    {
        this.userId = userId;
    }

    public async UniTask UpdateUserData()
    {
        UIManager.Instance.OpenLoadingPanel();

        try
        {
            //Create a request.
            GetUserDataRequest request = new()
            {
                userId = userId,
            };

            //request increase user diamonds.
            GetUserDataResponse response = await ServerAPI.Instance.userDataService.SendGetUserDataRequest(request);

            userDiamonds = response.userData.userDiamonds;
            userHeart = response.userData.userHearts;

            UIManager.Instance.CloseLoadingPanel();

            onServerDataUpdate?.Invoke(userDiamonds, userHeart);
        }
        catch (Exception exception)
        {
            UIManager.Instance.CloseLoadingPanel();

            ExceptionDisplayer.Show(exception);
        }
    }

    /// <summary>
    /// Use this function to modify to update user diamond both client and user.
    /// 
    /// ****
    /// in real case don't allow user to modify amount form client side, 
    /// Client should only send reward id to server and server will increase user currency follow reward id and response back to client
    /// ****
    /// 
    /// </summary>
    /// <returns></returns>
    public async UniTask ModifyDiamonds(int amount)
    {
        UIManager.Instance.OpenLoadingPanel();

        try
        {
            //Create a request.
            UserModifyDiamonRequest request = new()
            {
                userId = userId,
                amount = amount,
            };

            //request increase user diamonds.
            await ServerAPI.Instance.userDataService.SendModifyDiamondRequest(request);

            //Update client side, not need to get data form server for update every time.
            userDiamonds += amount;

            UIManager.Instance.CloseLoadingPanel();

            onServerDataUpdate?.Invoke(userDiamonds, userHeart);
        }
        catch (Exception exception)
        {
            UIManager.Instance.CloseLoadingPanel();

            ExceptionDisplayer.Show(exception);
        }
    }

    /// <summary>
    /// Use this function to modify to update user heart both client and user.
    /// 
    /// ****
    /// in real case don't allow user to modify amount form client side,
    /// ****
    /// 
    /// </summary>
    /// <returns></returns>
    public async UniTask ModifyHearts(int amount)
    {
        int finalHeart = amount + userHeart;

        //Prevent user heart over 100.
        //***In real case server should check the both on server side but.
        if (finalHeart > MaxHearts)
        {
            amount = MaxHearts - userHeart;
        }

        //Prevent user heart below 0.
        //***In real case server should check the same on server side.
        if (finalHeart < MinHearts)
        {
            amount = -userHeart;
        }

        UnityEngine.Debug.Log(amount);

        UIManager.Instance.OpenLoadingPanel();

        try
        {
            //Create a request.
            UserModifyHeartRequest request = new()
            {
                userId = userId,
                amount = amount,
            };

            //request increase user diamonds.
            await ServerAPI.Instance.userDataService.SendModifyHeartRequest(request);

            //Update client side, not need to get data form server for update every time.
            userHeart += amount;

            UIManager.Instance.CloseLoadingPanel();

            onServerDataUpdate?.Invoke(userDiamonds, userHeart);
        }
        catch (Exception exception)
        {
            UIManager.Instance.CloseLoadingPanel();

            ExceptionDisplayer.Show(exception);
        }
    }
}

public delegate void OnUserDataUpdate(int diamonds, int hearts);