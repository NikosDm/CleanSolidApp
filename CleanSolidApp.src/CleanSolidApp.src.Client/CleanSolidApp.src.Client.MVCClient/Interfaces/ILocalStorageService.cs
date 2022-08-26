using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanSolidApp.src.Client.MVCClient.Interfaces;

public interface ILocalStorageService
{
    void ClearStorage(List<string> keys);
    bool Exists(string key);
    T GetStorageValue<T>(string key);
    void SetStorageValue<T>(string key, T value);
}
