using AngleSharp.Common;
using Blazored.LocalStorage;
using Common.Services;
using Microsoft.AspNetCore.Components;

namespace ProjectUnitTests.ServiceTests
{
    public class AuthenticationStateTests
    {
        private AuthenticationStateService _service;

        [SetUp]
        public void Setup()
        {
            var storage = new MockLocalStorage();
            var navmanager = new MockNavigationManager();
            _service = new AuthenticationStateService(storage,navmanager);
        }

        [Test]
        public void Test_IsNotLoggedInIfNoJwtToken()
        {
            Assert.That(!_service.IsLoggedIn());
        }

        [Test]
        public void Test_IsLoggedInIfJwtToken()
        {
            _service.AddJwtToken("testtoken");
            Assert.That(_service.IsLoggedIn());
            _service.DeleteJwtToken();
        }

        [Test]
        public void Test_GetJwtToken()
        {
            _service.AddJwtToken("testtoken");
            var token = _service.GetJwtToken();
            Assert.That(token == "testtoken");
            _service.DeleteJwtToken();
        }

        [Test]
        public void Test_RemoveJwtToken()
        {
            _service.AddJwtToken("testtoken");
            _service.DeleteJwtToken();
            Assert.That(!_service.IsLoggedIn());
        }
    }

    public class MockNavigationManager : NavigationManager
    {
        protected override void NavigateToCore(string uri, bool forceLoad)
        {
            
        }

        protected override void NavigateToCore(string uri, NavigationOptions options)
        {
            
        }
    }

    public class MockLocalStorage : ISyncLocalStorageService
    {
        public event EventHandler<ChangingEventArgs> Changing;
        public event EventHandler<ChangedEventArgs> Changed;

        public Dictionary<string, object> StorageMock { get; } = new Dictionary<string, object>();

        public void Clear()
        {
            StorageMock.Clear();
        }

        public bool ContainKey(string key)
        {
            return StorageMock.ContainsKey(key);
        }

        public T GetItem<T>(string key)
        {
            var result = StorageMock.TryGetValue(key, out var value);
            if (!result || value == null!)
                return default(T)!;

            return (T)value;
        }

        public string GetItemAsString(string key)
        {
            var result = StorageMock.TryGetValue(key, out var value);
            if (!result || value == null!)
                return string.Empty;

            return value.ToString()!;
        }

        public string Key(int index)
        {
            return StorageMock.GetItemByIndex(index).Key;
        }

        public IEnumerable<string> Keys()
        {
            return StorageMock.Keys;
        }

        public int Length()
        {
            return StorageMock.Count;
        }

        public void RemoveItem(string key)
        {
            StorageMock.Remove(key);
        }

        public void RemoveItems(IEnumerable<string> keys)
        {
            foreach(var key in keys)
            {
                StorageMock.Remove(key);
            }
        }

        public void SetItem<T>(string key, T data)
        {
            if(data != null!)
                StorageMock.Add(key, data);
        }

        public void SetItemAsString(string key, string data)
        {
            StorageMock.Add(key, data);
        }
    }
}
