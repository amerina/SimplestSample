# SimplestSample

目标是.NET应用程序常用架构的最简实现



### May Be The Simplest .NETCore+CQRS Samples.

[SimplestSample/SimplestCQRS](https://github.com/amerina/SimplestSample/tree/main/SimplestCQRS)

思想：

CQRS（Command Query Responsibility Segregation）的主要思想是将一个系统分为两个部分：命令部分和查询部分。命令部分负责处理系统中的写操作，例如创建、更新和删除数据。查询部分则负责处理读操作，例如获取数据和计算汇总信息。两个部分之间通过事件驱动的方式进行通信，保证数据的一致性。**CQRS 的目标是将系统中复杂的处理逻辑拆分开来，提高各部分的可复用性和可维护性。**



参考：[MediatR Wiki (github.com)](https://github.com/jbogard/MediatR/wiki)



### May Be The Simplest .NETCore+GraphQLSamples.

[SimplestSample/SimplestGraphQL](https://github.com/amerina/SimplestSample/tree/main/SimplestGraphQL)

思想：

GraphQL的主要思想是提供一种更高效、更灵活的方式来进行API查询和数据获取。相比传统的REST API，**GraphQL允许客户端直接定义所需的数据结构和字段，并且可以在一次请求中获取所有需要的数据，避免了多次请求和响应的问题。**同时，GraphQL还支持强类型定义和查询语言，以及可以在服务器端进行数据转换和处理的高度可扩展性。最终，GraphQL可以提高代码开发效率、降低网络负载和提升应用性能。



GraphQL.Server.Ui.Altair

GraphQL.Server.Ui.Altair可以让你在应用程序中轻松地添加Altair GraphQL客户端，来测试和优化你的GraphQL实现。

要使用GraphQL.Server.Ui.Altair，你只需要在你的Startup.cs中添加一行代码：

```
//Altair就会在/ui/altair端点上运行
app.UseGraphQLAltair();
```



参考：[GraphQL .NET (graphql-dotnet.github.io)](https://graphql-dotnet.github.io/docs/getting-started/installation)

[Explore GraphQL: The API for modern apps.](https://www.graphql.com/)

[Getting Started with GraphQL in ASP.NET Core](https://codewithmukesh.com/blog/graphql-in-aspnet-core/)
