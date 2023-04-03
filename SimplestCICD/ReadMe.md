

[TOC]

[原文]:[Building A DevOps CI/CD Pipeline For ASP.NET Core With VSTS](https://www.c-sharpcorner.com/article/building-a-devops-cicd-pipeline-for-asp-net-core-with-vsts/)

一种基于 Web 的应用程序能够使我们的工作变得更加简单。特别是当我们需要在屏幕间进行交互时，我们可以考虑采用单页面应用程序（SPA）。对于 SPA，我们可能需要使用 Webpack、Grunt 或 Gulp 来合并和打包客户端脚本。假设我们使用 ASP.NET Core，则可能需要使用 npm、bower 或 yarn 等管理软件包，以便我们可以还原软件包并编译我们的应用程序。但是，如果您没有自动化的 CI/CD，则第一次部署单页面应用程序也很难。本文的主要重点是向您展示如何使用 VSTS 创建构建和发布ASP.NET Core 应用程序。

#### 覆盖主题

- 单体架构和微服务架构
- 敏捷和DevOps
- 持续集成
- 持续交付
- 持续部署
- 使用VSTS创建项目
- 创建新的构建定义
- 使用webpack捆绑脚本
- 创建新的发布定义

让我们深入研究基本概念，

#### 单体架构和微服务架构

曾经有一个时期，单体架构非常常见，瀑布模型很受欢迎。在这种方法论下，所有事情都被视为一个大项目，这是一个结构化和顺序性的过程。现在，它被分成了小的部分，并且采用了迭代方式。是的，我指的是微服务架构和敏捷方法论。

![OneBigProject](Image\OneBigProject.png)

#### 敏捷开发和DevOps

我们知道敏捷是一种开发方法论。而DevOps这个词则是敏捷的扩展，主要关注的是从开发过程到生产交付的流程。我们可以说，这是软件开发和运营的组合。

![AgileDevelopment](Image\AgileDevelopment.png)

#### DevOps的驱动力

当我们谈到DevOps时经常出现的词汇有：

- 持续集成（CI）
- 持续交付（CD）
- 持续部署（CD）

#### 持续集成

在一天中的多个时间点，开发人员会将他们的代码check-in/commit以及合并提交到一个共享代码库（如Git，Team Foundation Version Control等）。当这些代码合并后，会自动构建和运行自动化测试。

![CI_Process](Image\CI_Process.png)



#### 持续交付

代码集成后，代码将进行编译和测试。现在，它已准备好将代码推送到暂存环境（非生产测试）或生产环境，但您需要手工合并来推送它。

![CD_Manual_Process](Image\CD_Manual_Process.png)



#### 持续部署

在代码集成后，对其进行构建和测试。现在，在没有任何手工合并的情况下，它会自动推送到预发布环境（非生产测试）或生产环境。

![CD-Delivery](Image\CD-Delivery.png)



#### ASP.NET Core应用程序使用VSTS的自动化CI

##### 先决条件

您需要Visual Studio Team Services帐户和Git帐户。 您可以从https://www.visualstudio.com和https://github.com创建一个新的免费帐户。

#### 使用VSTS创建团队项目

1. 选择项目>新建项目

   ![Project](Image\Project.jpg)

2. 为项目提供一个名称，并为您的项目选择正确的版本控制-Git或TFVC。选择一个工作项流程。单击“创建”按钮创建项目

   ![CreateProject](Image\CreateProject.jpg)

3. 在Visual Studio中点击克隆

   ![Clone](Image\Clone.jpg)

4. VS IDE会弹出一个窗口。点击“克隆”按钮。

   ![VS_Clone](Image\VS_Clone.jpg)

5. 选择团队资源管理器，然后点击创建新项目或解决方案

   ![CreateSolution](Image\CreateSolution.jpg)

   创建项目后，您需要提交所有更改。单击“团队资源管理器>更改”。填写更改评论，然后单击“提交全部”

   ![Commit](Image\Commit.jpg)

6. 点击同步以同步代码

   ![sync](Image\sync.jpg)

7. 最后，点击“发布”（传出提交>发布）

   ![Publish](Image\Publish.jpg)



#### 为ASP.NET Core应用程序创建新的构建定义

1. 选择“构建和发布”选项卡和“构建”

2. 选择“新建”以创建新的定义

   ![1Step2_Build_Defination](Image\1Step2_Build_Defination.png)

3. 选择项目、代码库、分支，然后点击继续

   ![2Step3_SelectSource](Image\2Step3_SelectSource.jpg)

4. 根据您的项目类型选择一个构建模板。我选择 ASP.NET Core (.NET Framework)，并单击“应用”按钮以应用构建定义

   ![3Step4_TemplateForNewBuildDefination](Image\3Step4_TemplateForNewBuildDefination.png)

5. 选择进程任务(Process task)并填写构建定义名称（例如 HelloWorld-Dev，在发布中将使用此名称）选择您要运行构建的默认代理的 Hosted VS2022。

   如果您的团队使用 Visual Studio 2022，则选择 Hosted VS2022，因为它具有 Dot Net 核心框架和构建项目所需的其他组件。如果您的团队在 Ubuntu 上使用开发工具，则选择 Hosted Linux。如果您的团队使用的是 Visual Studio 2017 或 Visual Studio 2019，则选择 Hosted。

   选择“解决方案路径或 packages.config 的路径”中的项目解决方案文件并填写工件名称。

   ![5_1Step5_NamingOfBuildDefination](Image\5_1Step5_NamingOfBuildDefination.jpg)

6. 选择“获取资源”任务，并检查正确的项目、仓库和分支。选择“clean: true”和“clean options: sources”。标记资源为：从不。报告：构建状态

   ![6Step_6_GetSource](Image\6Step_6_GetSource.jpg)

7. 从第1阶段选择构建解决方案任务。选择Visual Studio版本：最新版。MSBuild架构：选择X64或X86，选择适合您的正确选项。

   ![7Step7_BuildSetup](Image\7Step7_BuildSetup.jpg)

   

#### Webpack应用程序构建和打包

1. 我们需要添加npm来安装我们项目中使用的所有软件包。要添加npm任务，请单击第1阶段右侧的（+）添加任务，然后选择“软件包”。查找并添加npm任务。现在，拖放npm任务以将其放置在构建解决方案之前。如果您使用Bower而不是npm，则需要添加Bower。

   ![8Step8_AddNPM](Image\8Step8_AddNPM.jpg)

   现在，选择npm任务并选择Command：install。现在，对于“Working folder with package.json”，选择包含根文件夹的主项目，其中有一个package.json文件。Select Custom registries and authentication Registries>Registries to use: Registries in my .npmrc; Advance>Verbose logging: checked; Control Options>Enabled: checked.

   ![8_2Step8_NPMInstall](Image\8_2Step8_NPMInstall.jpg)

   

2. 接下来，我们需要使用PowerShell脚本任务来编译客户端脚本。在这里，我们将安装Web Pack并使用Web Pack编译javascript。 选择(+)添加任务，然后从“生成”部分查找PowerShell。将其添加到生成定义中，位于npm安装和构建解决方案任务之间。

   ![9Step9_PowerShell](Image\9Step9_PowerShell.jpg)

   请填写以下信息，

   - 显示名称：PowerShellScript。

   - 类型：内联脚本。

   - 内联脚本

     ```
     (Get-Item -Path ".\" -Verbose).FullName  
     $env:Path=[System.Environment]::GetEnvironmentVariable("Path","Machine")+";"+  
     [System.Environment]::GetEnvironmentVariable("Path","User")  
     Get-Command -CommandType Application -ErrorAction SilentlyContinue -Name webpack | Select-Object -ExpandProperty Definition | echo  
     npm install -g webpack --no-optional  
     node_modules\.bin\webpack -p  
     ```

   - 高级 > 工作文件夹: 拥有 webpack.config.js 文件的 Web 应用程序的根文件夹。

   - 标准错误失败：Checked

   - 控制选项：启用已勾选 & 错误继续进行已勾选

   - 运行此任务：只有在所有先前的任务均已成功完成时才运行。

3. 选择“触发器”选项卡，选择以下信息。

   - 启用持续集成

   - 在构建过程中批量更改

   - 分支筛选器>类型：包括和分支规范：例如dev或QA或master

     ![10Step10_Triggers](Image\10Step10_Triggers.jpg)

4. 选择“选项”选项卡并选择以下信息。

   - 新建生成请求处理：启用。

   - 自动链接此生成中的新工作：启用。

   - 仅链接添加到规范分支的工作>类型：包括；分支规范：即dev。

   - 您还可以更改Build job的默认值

     ![11Step_11_Options](Image\11Step_11_Options.jpg)

5. 最终，点击“Save & queue\>Save”以保存构建定义

   ![12Step12_SaveBuildDefination](Image\12Step12_SaveBuildDefination.jpg)



现在，我们已经实现了自动化的持续集成构建，如果您将代码更改提交到您选择的分支中，则会看到以下输出。

![13_Step13_Build_Output](Image\13_Step13_Build_Output.jpg)



#### 创建新的发布定义 

1. 转到构建和发布选项卡 > 发布 > 新定义

   ![1.NewRelease](Image\1.NewRelease.jpg)

   **注意**
   如果您已经有现有的发布，请选择加号（+）并创建发布定义。

   ![1_2CreateNewRelease](Image\1_2CreateNewRelease.jpg)

2. 选择一个模板 - IIS网站和SQL数据库部署

   ![2.IIS_TemplateAdd_Release](Image\2.IIS_TemplateAdd_Release.jpg)

3. 您将得到一个环境窗口。现在，从属性中更改环境名称。

   ![3Environment_Name](Image\3Environment_Name.jpg)

4. 在Artifacts面板中，选择+ 添加并选择项目，源（生成定义），默认版本和源别名的名称。 点击添加按钮。

   ![4Release_Add_Artifact](Image\4Release_Add_Artifact.jpg)

5. 点击闪电图标以触发持续部署，然后在右侧启用它。如果您希望在源工件的新版本可用时创建新的发布，则需要它。点击添加按钮，然后选择类型和构建分支。

   ![5CI_Trigger](Image\5CI_Trigger.jpg)

6. 点击“环境”的闪电符号，然后

   - 选择触发器：发布后

   - 构建物过滤器：启用

   - 选择+添加>构建物名称（例如HelloWorld-Artifact-Dev）

   - 类型：包括；构建分支：例如Dev；构建标签：留空。

     ![6Trigger_Env](Image\6Trigger_Env.jpg)

7. 在浏览器左侧选择任务或从环境面板中选择任务（2个阶段，2个任务）。这些任务将执行您的部署过程。

   ![7Task_Env](Image\7Task_Env.jpg)

8. 现在选择任务>任务环境（例如，HelloWorld-Dev-Env），并填写以下信息，

   - 配置类型：IIS网站

   - 操作：创建或更新

   - 网站名称：HelloWorldApp-Dev

   - 应用程序池>名称：HelloWorldApp-Dev

   - 单击“添加绑定”>...按钮。添加绑定窗口将弹出并填写信息：

   - 协议：HTTP或HTTPS，端口：您的IIS分配端口（即543）和主机名：例如helloWorldApp-dev.yourDomain.com。

     ![8Task_Env_1](Image\8Task_Env_1.jpg)

9. 单击左侧的IIS部署，然后选择部署组。

   ![9IIS_Deployment_Group](Image\9IIS_Deployment_Group.jpg)

10. 选择IIS Web应用程序管理并填写信息，

    - 物理路径：％SystemDrive％\inetpub\wwwroot\Dev\HelloWorldApp-Dev\

    - 物理路径验证：应用程序用户（透过）

    - .NET版本：v4.0

    - 托管管道模式：集成

    - 身份：选择您喜欢的身份。

      ![10IIS_Web_App_Manage](Image\10IIS_Web_App_Manage.jpg)

11. 选择IIS Web应用程序部署并选中“Take App Offline”。

    ![11IIS_Web_App_Deploy](Image\11IIS_Web_App_Deploy.jpg)

12. 我没有使用 SQL 部署任务。因此，如果您不需要它，可以禁用或删除该任务。选择 SQL 部署，右键单击以禁用选择的任务或删除选择的任务。

    ![12Disable_SQL_DB](Image\12Disable_SQL_DB.jpg)

13. 最后，点击“保存”按钮保存发布定义。

**注意事项**
请勿忘记设置您的IIS配置。







