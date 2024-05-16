﻿namespace Maomi.MQ
{
    public interface IConsumer { }
    public interface IConsumer<TEvent> : IConsumer
        where TEvent : class
    {
        public Task ExecuteAsync(EventBody<TEvent> message);

        // 增加重试停止接口，以便在重试或放弃消息时通知

        // 每次重试失败
        public Task FaildAsync(EventBody<TEvent>? message);

        // 多次重试失败后
        public Task FallbackAsync(EventBody<TEvent>? message);
    }
}
