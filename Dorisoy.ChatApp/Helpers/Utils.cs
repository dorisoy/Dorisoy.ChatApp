namespace Dorisoy.ChatApp.Helpers;

public static class Utils
{
    public static string GetUserId(string chatUsername)
    {
        string[] datas = chatUsername.Split('_');
        foreach (string data in datas)
        {
            return data;
        }

        return null;
    }

    public static string GetName(string chatUsername)
    {
        int count = 1;
        string[] datas = chatUsername.Split('_');
        foreach (string data in datas)
        {
            if (count == 4)
            {
                return data;
            }

            count++;
        }

        return null;
    }
}
