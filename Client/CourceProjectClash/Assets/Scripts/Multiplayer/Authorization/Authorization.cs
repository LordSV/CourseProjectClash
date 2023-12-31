using System;
using System.Collections.Generic;
using UnityEngine;

public class Authorization : MonoBehaviour
{
    private const string LOGIN = "login";
    private const string PASSWORD = "password";
    private string _login;
    private string _password;

    public event Action Error;

    public void SetLogin(string login)
    {
        _login = login;
    }
    public void SetPassword(string password)
    {
        _password = password;
    }

    public void SignIn()
    {
        if(string.IsNullOrEmpty(_login) || string.IsNullOrEmpty(_password))
        {
            ErrorMessage("����� �/��� ������ ������");
            return;
        }
        string uri = URLLibrary.MAIN + URLLibrary.AUTHORIZATION;
        Dictionary<string, string> data = new Dictionary<string, string>()
        {
            {LOGIN, _login },
            {PASSWORD, _password }
        };
        Network.Instance.Post(uri, data, Success, ErrorMessage);
    }

    private void Success(string data)
    {
        string[] result = data.Split('|');
        if(result.Length < 2 || result[0] != "ok")
        {
            ErrorMessage("����� �� ������� ������ ��� �����: " +  data);
            return;
        }
        if (int.TryParse(result[1], out int id))
        {
            UserInfo.Instance.SetID(id);
            Debug.Log("�������� ����, ID = " + id);
        }
        else
        {
            ErrorMessage($"�� ������� ���������� \"{result[1]}\" � INT. ������ ����� ��� �����: {data}");
        }
    }

    public void ErrorMessage(string error)
    {
        Debug.LogError(error);
        Error?.Invoke();
    }
}
