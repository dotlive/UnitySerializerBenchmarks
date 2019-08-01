using MessagePack;
using Newtonsoft.Json;
using OdinSerializer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class test : MonoBehaviour
{
    [Serializable]
    [MessagePackObject]
    public class No1
    {
        [Key(0)]
        public int no1;
        [Key(1)]
        public string hehe = "hehe";
    }
    [Serializable]
    [MessagePackObject]
    public class No2 : No1
    {
        [Key(0)]
        public int no2;
    }
    [Serializable]
    [MessagePackObject]
    public class Set
    {
        [Key(0)]
        public List<No1> set = new List<No1>();
    }
    // Start is called before the first frame update
    void Start()
    {
        var stream = new MemoryStream();
        var set = new Set();
        for(int i = 0; i < 10; i++)
        {
            set.set.Add(new No1());
            set.set.Add(new No2());
        }
       
        List<Type> types = new List<Type>();
        types.Add(typeof(No1));
        types.Add(typeof(No2));
        types.Add(typeof(Set));
        var sw = Stopwatch.StartNew();

        BinaryFormatter b = new BinaryFormatter();
        stream = new MemoryStream();
        sw.Restart();
        for (int i = 0; i < 1000; i++)
        {
            stream.Seek(0, SeekOrigin.Begin);
            b.Serialize(stream, set);
        }
        sw.Stop();
        Debug.LogFormat("BinaryFormatter Writing {0} ms", sw.ElapsedMilliseconds);

        sw.Restart();
        for (int i = 0; i < 1000; i++)
        {
            stream.Seek(0, SeekOrigin.Begin);
            var newobj = b.Deserialize(stream);
        }
        sw.Stop();
        Debug.LogFormat("BinaryFormatter Reading {0} ms", sw.ElapsedMilliseconds);

        byte[] bytes = null;
        sw.Restart();
        for (int i = 0; i < 1000; i++)
        {
            bytes = SerializationUtility.SerializeValue<Set>(set, DataFormat.Binary);
        }
        sw.Stop();
        Debug.LogFormat("OdinSerializer Writing {0} ms", sw.ElapsedMilliseconds);

        sw.Restart();
        for (int i = 0; i < 1000; i++)
        {
            Set newset = SerializationUtility.DeserializeValue<Set>(bytes, DataFormat.Binary);
        }
        sw.Stop();
        Debug.LogFormat("OdinSerializer Reading {0} ms", sw.ElapsedMilliseconds);


        string str = "";

        sw.Restart();
        for (int i = 0; i < 1000; i++)
        {
            str = JsonUtility.ToJson(set, true);
        }
        sw.Stop();
        Debug.LogFormat("JsonUtility Writing {0} ms", sw.ElapsedMilliseconds);

        
        sw.Restart();
        for (int i = 0; i < 1000; i++)
        {
            Set newSet = JsonUtility.FromJson<Set>(str);
        }
        sw.Stop();
        Debug.LogFormat("JsonUtility Reading {0} ms", sw.ElapsedMilliseconds);

#if !ENABLE_IL2CPP

        sw.Restart();
        for (int i = 0; i < 1000; i++)
        {
            bytes = MessagePack.MessagePackSerializer.Serialize<Set>(set);
        }
        sw.Stop();
        Debug.LogFormat("MessagePack Writing {0} ms", sw.ElapsedMilliseconds);

        sw.Restart();
        for (int i = 0; i < 1000; i++)
        {
            Set newSet = MessagePack.MessagePackSerializer.Deserialize<Set>(bytes);
        }
        sw.Stop();
        Debug.LogFormat("MessagePack Reading {0} ms", sw.ElapsedMilliseconds);

        var ser = new NetSerializer.Serializer(types);
        stream = new MemoryStream();
        sw.Restart();
        for (int i = 0; i < 1000; i++)
        {
            stream.Seek(0, SeekOrigin.Begin);
            ser.Serialize(stream, set);
        }
        sw.Stop();
        Debug.LogFormat("NetSerializer Writing {0} ms", sw.ElapsedMilliseconds);


        sw.Restart();
        for (int i = 0; i < 1000; i++)
        {
            stream.Seek(0, SeekOrigin.Begin);
            var newobj = ser.Deserialize(stream);
        }
        sw.Stop();
        Debug.LogFormat("NetSerializer Reading {0} ms", sw.ElapsedMilliseconds);

        str = "";
        sw.Restart();
        for (int i = 0; i < 1000; i++)
        {
            str = JsonConvert.SerializeObject(set);
        }
        sw.Stop();
        Debug.LogFormat("Newtonsoft.Json Writing {0} ms", sw.ElapsedMilliseconds);


        sw.Restart();
        for (int i = 0; i < 1000; i++)
        {
            Set newset = JsonConvert.DeserializeObject<Set>(str);
        }
        sw.Stop();
        Debug.LogFormat("Newtonsoft.Json Reading {0} ms", sw.ElapsedMilliseconds);

#endif
        Console.ReadLine();
    }
}
