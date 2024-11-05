using MedCom.EasySocket.SocketCom.Core;
using System;
using System.Collections.Generic;



public class SubArrayMatcher
{
    private static int[] BuildLpsArray(byte[] pattern)
    {
        int[] lps = new int[pattern.Length];
        int length = 0;
        int i = 1;

        while (i < pattern.Length)
        {
            if (pattern[i] == pattern[length])
            {
                length++;
                lps[i] = length;
                i++;
            }
            else
            {
                if (length != 0)
                {
                    length = lps[length - 1];
                }
                else
                {
                    lps[i] = 0;
                    i++;
                }
            }
        }

        return lps;
    }

    private static int KmpSearch(byte[] text, byte[] pattern)
    {
        int[] lps = BuildLpsArray(pattern);
        int i = 0;
        int j = 0;

        while (i < text.Length)
        {
            if (pattern[j] == text[i])
            {
                i++;
                j++;
            }

            if (j == pattern.Length)
            {
                return i - j;
            }
            else if (i < text.Length && pattern[j] != text[i])
            {
                if (j != 0)
                {
                    j = lps[j - 1];
                }
                else
                {
                    i++;
                }
            }
        }
        return -1;
    }

    public static Dictionary<int, ByteType> FindSubArrays(byte[] M, byte[] A, byte[] B)
    {
        Dictionary<int, ByteType> byteDict = new Dictionary<int, ByteType>();

        int startIndexA = KmpSearch(M, A);
        if (startIndexA != -1)
        {
            byteDict[startIndexA + A.Length - 1] = ByteType.StartPst;
        }

        int startIndexB = KmpSearch(M, B);
        if (startIndexB != -1)
        {
            byteDict[startIndexB] = ByteType.EndPst;
        }

        return byteDict;
    }
}