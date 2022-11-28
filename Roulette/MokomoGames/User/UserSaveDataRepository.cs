using UnityEngine;

namespace MokomoGames.User
{
    public class UserData
    {
        public string UserId;
    }

    public class UserSaveDataRepository
    {
        private const string UserIdKey = "UserId";
        public bool SaveExists()
        {
            return PlayerPrefs.HasKey(UserIdKey);
        }

        public void Save(UserData userData)
        {
            if (!PlayerPrefs.HasKey(UserIdKey))
            {
                PlayerPrefs.SetString(UserIdKey, userData.UserId);
            }
            PlayerPrefs.Save();
        }

        public UserData Load()
        {
            var userData = new UserData();
            userData.UserId = PlayerPrefs.GetString(UserIdKey);
            return userData;
        }
    }
}