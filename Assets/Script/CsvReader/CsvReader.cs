using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class CsvReader<T>
{
    private static TextAsset csvFile;
    
    public static List<T> GetList(string filePath)
    {
        csvFile = Resources.Load(filePath) as TextAsset;
        StringReader reader = new StringReader(csvFile.text);
        List<string[]> rowData = new List<string[]>();
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            rowData.Add(line.Split(','));
        }

        List<T> entities = new List<T>();
        var fieldInfos = typeof(T).GetFields();

        for(int i = 1; i < rowData.Count; i++)
        {
            dynamic entity = Activator.CreateInstance(typeof(T));
            for (int j = 0; j < fieldInfos.Length; j++)
            {               
                var field = typeof(T).GetField(fieldInfos[j].Name);
                switch (field.FieldType)
                {
                    case Type _ when field.FieldType == typeof(int):
                        field.SetValue(entity, Int32.Parse(rowData[i][j]));
                        break;
                    case Type _ when field.FieldType == typeof(string):
                        field.SetValue(entity, rowData[i][j]);
                        break;
                    default:
                        break;
                }
            }
            entities.Add(entity);
        }
        return entities;
    }
}
