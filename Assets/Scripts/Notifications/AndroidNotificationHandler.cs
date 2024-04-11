using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;
public class AndroidNotificationHandler : MonoBehaviour
{
#if UNITY_ANDROID
    public static AndroidNotificationHandler Instance { get; private set; }

    private const string ChannelId = "notification_channel";

    private void Awake()
    {
        Instance = this;
    }

    public void ScheduleNotification(DateTime dateTime)
    {
        AndroidNotificationCenter.CancelAllDisplayedNotifications();

        AndroidNotificationChannel notificationChannel = new AndroidNotificationChannel
        {
            Id = ChannelId,
            Name = "Notification Channel",
            Description = "Some random description",
            Importance = Importance.High
        };

        AndroidNotificationCenter.RegisterNotificationChannel(notificationChannel);

        AndroidNotification notification = new AndroidNotification
        {
            Title = "Come back!",
            Text = "Red Crates are back! Return and unlock new skins and upgrades!",
            SmallIcon = "Icon_0",
            LargeIcon = "Default",
            FireTime = dateTime
        };

        int id = AndroidNotificationCenter.SendNotification(notification, ChannelId);

        if(AndroidNotificationCenter.CheckScheduledNotificationStatus(id) == NotificationStatus.Scheduled)
        {
            AndroidNotificationCenter.CancelAllNotifications();
            AndroidNotificationCenter.SendNotification(notification, ChannelId);
        }
    }
#endif
}
