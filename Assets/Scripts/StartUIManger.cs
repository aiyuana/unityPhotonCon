using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using Common2;
using UnityEngine.SceneManagement;
public class StartUIManger : MonoBehaviour
{
    public static StartUIManger Instance;
    public Transform m_LoginPanel, m_RegisterPanel, m_MainPanel;
    public GameObject m_HintMessage;
    private InputField username_Login, age_Login, userage_Reister, age_Register;

    private void Awake()
    {
        Instance = this;
        InitIf();
        InitBin();
    }
private void InitIf()
{
    username_Login = m_LoginPanel.Find("InputField").GetComponent<InputField>();
    age_Login=m_LoginPanel.Find("InputPassword").GetComponent<InputField>();
    userage_Reister=m_RegisterPanel.Find("InputField").GetComponent<InputField>();
    age_Register=m_RegisterPanel.Find("InputPassword").GetComponent<InputField>();
}

private void ShowHint(string str)
{
    m_HintMessage.SetActive(true);
    m_HintMessage.GetComponent<Text>().text = str;
}

private void InitContent()
{
    username_Login.text = "";
    age_Login.text = "";
    
}
private void InitContentRegister()
{
    userage_Reister.text = "";
    age_Register.text= "";
}
private void InitBin()
{
    #region log
    m_LoginPanel.Find("LoginBtn").GetComponent<Button>().onClick.AddListener(() =>
    {
        //登录按钮
        //向服务器发生一个事件，测试
        if(username_Login.text.Equals("")||age_Login.text.Equals(""))
        {
            InitContent();
            ShowHint("输入为空请重新输入");
            return;
        }
        
        
        Dictionary<byte, object> data = new Dictionary<byte, object>();
        data.Add((byte)parameterCode.Username,username_Login.text);
        data.Add((byte)parameterCode.age,age_Login.text);
        PhotonManager.instence.Peer.OpCustom((byte)operationcode.Login, data,true);
    });
    m_LoginPanel.Find("LoginBtn (1)").GetComponent<Button>().onClick.AddListener(() =>
    {
        //登录按钮
        //向服务器发生一个事件，测试
      m_LoginPanel.gameObject.SetActive(false);
      m_RegisterPanel.gameObject.SetActive(true);
    });
    #endregion
    #region Register
    m_RegisterPanel.Find("RegisterBtn").GetComponent<Button>().onClick.AddListener(() =>
    {
        //Register
        //Check Input before send message:NULL check
        if (userage_Reister.text.Equals("")||age_Register.text.Equals(""))
        {
            ShowHint("输入的内容不能为空，请重新输入");
            InitContentRegister();
            return;
        }
            
        Dictionary<byte, object> data = new Dictionary<byte, object>();
        data.Add((byte) parameterCode.Username,userage_Reister.text);
        data.Add((byte) parameterCode.age,age_Register.text);
        PhotonManager.instence.Peer.OpCustom((byte)operationcode.Register, data,true);
        
    });
        
    m_RegisterPanel.Find("RegisterBtn1").GetComponent<Button>().onClick.AddListener(() =>
    {
       m_LoginPanel.gameObject.SetActive(true);
       m_RegisterPanel.gameObject.SetActive(false);
    });
    #endregion
    m_MainPanel.Find("StartBtn (2)").GetComponent<Button>().onClick.AddListener(() =>
    {
        Dictionary<byte, object> data = new Dictionary<byte, object>();
        data.Add((byte)parameterCode.Username,username_Login.text);
        data.Add((byte)parameterCode.age,age_Login.text);
        PhotonManager.instence.Peer.OpCustom((byte)operationcode.Logout, data,true);
    });
    m_MainPanel.Find("StartBtn").GetComponent<Button>().onClick.AddListener(() =>
    {
        SceneManager.LoadScene("Game");
    });
}

public void OnLogin(ReturnCode returnCode)
{
    if (returnCode.Equals(ReturnCode.Success))
    {
        ShowHint("登录成功");
        m_LoginPanel.gameObject.SetActive(false);
        m_MainPanel.gameObject.SetActive(true);
        PhotonManager.instence.Username=username_Login.text;
    }
    else
    {
        ShowHint("登录失败，密码错误或者异地已登录");
        InitContent();
    }
}
public void OnRegister(ReturnCode returnCode)
{
    if (returnCode .Equals(ReturnCode.Success) )
    {
        //verify successfully
        ShowHint("注册成功");
        m_RegisterPanel.gameObject.SetActive(false);
        m_LoginPanel.gameObject.SetActive(true);
    }
    else
    {
        //login fail
        ShowHint("注册失败，请检查账号是否存在");
        InitContentRegister();
    }
}
public void OnLogOut(ReturnCode returnCode)
{
    if (returnCode .Equals(ReturnCode.Success) )
    {
        //verify successfully
        ShowHint("注销成功");
        InitContent();
        m_MainPanel.gameObject.SetActive(false);
        m_LoginPanel.gameObject.SetActive(true);
        
    }
    else
    {
        //login fail
        ShowHint("注销失败");
    }
}
}
