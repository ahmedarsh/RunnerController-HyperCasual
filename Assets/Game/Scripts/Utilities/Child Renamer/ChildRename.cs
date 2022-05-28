using UnityEngine;

public class ChildRename : MonoBehaviour
{
    public enum Zeros
    {
        Non_Zero,
        One_Zero,
        Two_Zero
    }

    public string Name = "Level %d";

    public Zeros zeros;

    string s;

    public void RenameAll()
    {
        for (int x = 0; x < transform.childCount; x++)
        {
            int index = x + 1;
            switch (zeros)
            {
                case Zeros.Non_Zero:
                    s = Name.Replace("%d", index.ToString());
                    break;
                case Zeros.One_Zero:
                    s = Name.Replace("%d", index.ToString("00"));
                    break;
                case Zeros.Two_Zero:
                    s = Name.Replace("%d", index.ToString("000"));
                    break;
            }

            transform.GetChild(x).name = string.Format("{0}", s);
        }
    }
}