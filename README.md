## Dorisoy.ChatApp

基于 .NET MAUI 开发的完整聊天应用程序。它遵循 MVVM 模式。该项目包括使用 SignalR 的 C# 服务器端 chathub 实现。您可以轻松地将此代码集成到现有或新项目中。

## 解决方案包含两个项目：

ChatServer（.NET SignalR 聊天中心服务器）
ChatApp（.NET MAUI 项目）


## 如何运行应用程序

步骤 1： 从 Visual Studio 运行“ChatServer”项目（它是一个 SignalR 聊天中心服务器）并获取服务器的 IP 地址。

步骤 2： 在项目 ChatApp/Helpers/ChatHelper.cs 文件下名为“GetInstance”的函数中设置 IP 地址

注意：如果您在本地主机中运行“ChatServer”，则可以通过 conveyor 插件获取 ip。[https://keyoti.com/products/conveyor/index.html] 安装此 visual studio 插件。然后运行“ChatServer”项目。现在单击 conveyor 提供的 IP 地址。

## 应用程序包包含：

适用于 Android、iOS 和 Windows 的 .NET MAUI 应用程序

采用 MVVM 模式构建

使用 SignalR 的服务器端实现

定义良好的 xaml 和 c# 代码

漂亮的 UI 设计

注册页面

登录页面

聊天用户列表页面

消息页面

打字指示器

在线/离线状态

完整的源代码


## 要求：

Visual Studio 2022

.NET >= 6

##  快速入门：

### Visual Studio 解决方案包含两个项目：

聊天应用程序
聊天服务器

### ChatApp项目实现了所有客户端功能：

#### 登录页面

在验证用户身份后，您必须调用ChatHub 服务器的 Connect函数，然后重定向到 ChatList 页面。

#### 聊天列表页面

在此页面上，您必须从 ChatServer 调用已连接用户列表。获取已连接用户列表后，您必须将其呈现到列表视图中。要发起聊天，您必须点击该项目以导航聊天页面。

#### 聊天页面

#### 您可以在此处向已连接的用户发送消息。此处实现了两个重要功能。

. 调用“ReceiveMessage”从对方获取消息。收到消息后，您必须以良好的方式呈现到列表视图。
. 调用“SendMessage”将消息发送给您的聊天用户。此处您必须传递必要的参数，如连接 ID、用户 ID、消息等。

#### ChatServer项目包含ChatHub.cs文件，其中实现了以下服务器端功能：

发送消息：在这个函数中，你可以发送消息给其他人。你必须传递接收用户ID和消息。

打字：此函数返回聊天用户的输入指示。这里您必须传递连接 ID 和您的姓名。

连接异步：当两个用户之间建立连接时，将调用此函数。在这里，您可以根据需要添加自定义实现。

断开连接时异步：当两个用户之间的连接断开时，将调用此函数。在这里，您可以根据需要添加自定义实现。

获取已连接用户：在此功能中您可以获取所有已连接的用户。

断开：要断开会话，您可以调用此函数。
