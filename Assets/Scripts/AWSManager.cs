using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.Runtime;
using System.IO;
using System;
using Amazon.S3.Util;
using System.Collections.Generic;
using Amazon.CognitoIdentity;
using Amazon;

public class AWSManager : MonoBehaviour
{
    private static AWSManager _instance;
    public static AWSManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("AWS Manager is null");
            return _instance;
        }
    }

    public GameObject targetImg;

    public string S3Region = RegionEndpoint.USEast2.SystemName;
    private RegionEndpoint _S3Region
    {
        get { return RegionEndpoint.GetBySystemName(S3Region); }
    }

    private AmazonS3Client _s3Client;
    public AmazonS3Client S3Client
    {
        get
        {
            if(_s3Client == null)
            {
                _s3Client = new AmazonS3Client(new CognitoAWSCredentials(
                    "us-east-1:f186267a-c94a-48e1-98fb-94c7ac00efbe",
                    RegionEndpoint.USEast1
                    ), _S3Region);
            }

            return _s3Client;
        }
    }

    private void Awake()
    {
        _instance = this;
        UnityInitializer.AttachToGameObject(this.gameObject);
        AWSConfigs.HttpClient = AWSConfigs.HttpClientOption.UnityWebRequest;

        var request = new ListObjectsRequest()
        {
            BucketName = "educationappar"
        };

        S3Client.ListObjectsAsync(request, (responseObj) =>
        {
            if(responseObj.Exception == null)
            {
                responseObj.Response.S3Objects.ForEach((obj) =>
                {
                    Debug.Log("Obj: " + obj.Key);
                });
            }
            else
            {
                Debug.LogWarning(responseObj.Exception);
            }
        });

        DownloadBundle();
    }

    public void DownloadBundle()
    {
        StartCoroutine(BundleRoutine());
    }

    IEnumerator BundleRoutine()
    {
        string url = "https://educationappar.s3.us-east-2.amazonaws.com/horse";
        using(UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(url))
        {
            yield return request.SendWebRequest();

            if(request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error);
            }
            else
            {
                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);
                GameObject horse = bundle.LoadAsset<GameObject>("horse");
                horse = Instantiate(horse);
                horse.transform.parent = targetImg.transform;
                horse.transform.position = new Vector3(-0.0989f, 0.017f, -0.024f);
                horse.transform.eulerAngles = new Vector3(0, 0, -90);
                horse.transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);
                horse.transform.parent.gameObject.SetActive(true);
                UIManager.Instance.anim = horse.transform.GetComponent<Animator>();
                
            }
        }
    }
}
