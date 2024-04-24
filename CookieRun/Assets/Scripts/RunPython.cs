using System;
using System.Diagnostics;
using System.Threading.Tasks;
using UnityEngine;

public class RunPython : MonoBehaviour
{
    private Process process;
    private bool isReadingOutput = false;

    void Update()
    {
        if (!isReadingOutput)
        {
            try
            {
                StartPythonProcess();
                Task.Run(() => ReadPythonOutputAsync());
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError("[오류] 예외 발생: " + e.Message);
            }
        }
    }

    private void StartPythonProcess()
    {
        process = new Process();
        process.StartInfo.FileName = @"C:/Python310/python.exe";
        process.StartInfo.Arguments = @"C:/Users/user/Desktop/CookieRun/CookieRun/Assets/Scripts/jump.py";
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;

        process.Start();
        UnityEngine.Debug.Log("[정보] Python 프로세스 시작");

        isReadingOutput = true;
    }

    private async Task ReadPythonOutputAsync()
    {
        while (!process.HasExited)
        {
            while (!process.StandardOutput.EndOfStream)
            {
                var output = await process.StandardOutput.ReadLineAsync();
                UnityEngine.Debug.Log("[Python 출력] " + output);
                if(GameManager.instance.jump == true) GameManager.instance.pyjump = true;
            }

            if (!process.StandardError.EndOfStream)
            {
                var error = await process.StandardError.ReadToEndAsync();
                if (!string.IsNullOrEmpty(error))
                {
                    // UnityEngine.Debug.LogError("[Python 오류] " + error);
                }
            }

            await Task.Delay(100);  // CPU 과부하 방지
        }

        isReadingOutput = false;  // 프로세스 종료 시 출력 읽기 중단
    }
}