# Continuous signal transmission with SignalR and Blazor

---

*A strategic example of a real-time signal monitoring application with Blazor.*

When we are going to talk about real time with .NET technologies, we are naturally talking about **SignalR**. Professionally I have programmed SignalR since its first version, in .NET Framework, engineering systems that are still in production. Currently SignalR with .NET Core gives us a much improved panorama, and it is one of the most reliable technologies in the real time paradigm.

This article presents an example of SignalR in a Blazor application. Beyond the classic *Chat* example, this example goes more towards a case of continuous communication from a broadcaster, which is technically known as *Broadcasting*. There are several ways to do this. In this case I present a simple technique, although, depending on the interest, I expanded it in a second installment to show other more refined and optimal techniques, such is the case of *Streaming* with SignalR together with the *MessagePack* protocol (it is replaced JSON over a binary format), which is more convenient in a real production case.

At the end we will have a Solution that looks like the following image (two projects running simultaneously).

This article is published in [Blazor Spread Blog](https://www.blazorspread.net/blogview/10) (languages EN, ES, DE)

Licencia MIT
