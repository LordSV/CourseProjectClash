using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Registration : MonoBehaviour
{
    private const string LOGIN = "login";
    private const string PASSWORD = "password";
    private string _login;
    private string _password;
    private string _confirmPassword;

    public event Action Error;
    public event Action Success;

    public void SetLogin(string login)
    {
        _login = login;
    }
    public void SetPassword(string password)
    {
        _password = password;
    }

    public void SetConfirmPassword(string confirmPassword)
    {
        _confirmPassword = confirmPassword;
    }

    public void SignUp()
    {
        if (string.IsNullOrEmpty(_login) || string.IsNullOrEmpty(_password) || string.IsNullOrEmpty(_confirmPassword))
        {
            ErrorMessage("����� �/��� ������ ������");
            return;
        }
        if(_password != _confirmPassword)
        {
            ErrorMessage($"������ �� ���������: {_password} != {_confirmPassword}");
            return;
        }
        string uri = URLLibrary.MAIN + URLLibrary.REGISTRATION;
        Dictionary<string, string> data = new Dictionary<string, string>()
        {
            {LOGIN, _login },
            {PASSWORD, _password }
        };
        Network.Instance.Post(uri, data, SuccessMessage, ErrorMessage);
    }

    private void SuccessMessage(string data)
    {
        if (data != "ok")
        {
            ErrorMessage("����� �� ������� ������ ��� �����: " + data);
            return;
        }
        Debug.Log("�������� �����������");
        Success?.Invoke();
    }

    public void ErrorMessage(string error)
    {
        Debug.LogError(error);
        Error?.Invoke();
    }
}
