using Application.MediatrEntities.Posts.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Notifications
{
    public class PostNotification : INotification
    {
        public CreatePostCommand? CreatePostCommand { get; init; }
    }
    public class PostNotificationHandler : INotificationHandler<PostNotification>
    {
        public Task Handle(PostNotification notification, CancellationToken cancellationToken)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(notification?.CreatePostCommand?.Name);
            Console.ResetColor();

            return Task.CompletedTask;
        }
    }
}
