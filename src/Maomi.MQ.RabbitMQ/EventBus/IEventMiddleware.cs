﻿// <copyright file="IEventMiddleware.cs" company="Maomi">
// Copyright (c) Maomi. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// Github link: https://github.com/whuanle/Maomi.MQ
// </copyright>

namespace Maomi.MQ.EventBus;

/// <summary>
/// Consumer abstract interface.
/// </summary>
/// <typeparam name="TMessage">事件模型.</typeparam>
public interface IEventMiddleware<TMessage>
{
    /// <summary>
    /// The received message is processed when it has been deserialized correctly.<br />
    /// 当消息被正确反序列化后，处理接收到的消息.
    /// </summary>
    /// <param name="messageHeader"></param>
    /// <param name="message">事件内容.</param>
    /// <param name="next">事件执行链委托.</param>
    /// <returns><see cref="Task"/>.</returns>
    Task ExecuteAsync(MessageHeader messageHeader, TMessage message, EventHandlerDelegate<TMessage> next);

    /// <summary>
    /// When an exception occurs to ExecuteAsync, execute it immediately.<br />
    /// 当 ExecuteAsync 出现异常后，立即执行.
    /// </summary>
    /// <param name="messageHeader"></param>
    /// <param name="ex">An anomaly occurs when consuming.<br />消费时出现的异常.</param>
    /// <param name="retryCount">Current retry times.<br />当前重试次数.</param>
    /// <param name="message"></param>
    /// <returns><see cref="Task"/>.</returns>
    public Task FaildAsync(MessageHeader messageHeader, Exception ex, int retryCount, TMessage? message);

    /// <summary>
    /// When all retries fail, or an exception occurs that the ExecuteAsync method cannot be accessed.<br />
    /// 当所有重试均失败，或出现不能进入 ExecuteAsync 方法的异常.
    /// </summary>
    /// <param name="messageHeader"></param>
    /// <param name="message"></param>
    /// <param name="ex"></param>
    /// <returns>Check whether the rollback is successful.</returns>
    public Task<ConsumerState> FallbackAsync(MessageHeader messageHeader, TMessage? message, Exception? ex);
}
