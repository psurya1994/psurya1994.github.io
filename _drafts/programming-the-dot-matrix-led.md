---
layout: post
title: "Programming A LED Matrix Display"
description: ""
headline: 
modified: 2016-04-07
category: Electronics
tags: [jekyll]
imagefeature: 
mathjax: 
chart: 
comments: true
featured: false
---

In this article, I talk about how we learnt to program a LED matrix whose protocol we did not know. [http://nikhilendra.com/](Nikhil) and I worked on this with the [http://almabase.com/](Almabase) team for building a digital dashboard named Abacus for them.

The LED dot matrix display that we were trying to program looked similar to this. It was a 96x16 dot matrix display.

![NGX-Display](/images/blog/dot-matrix-1.jpg)

The manufacturer provided us with a windows application to program this, and it's source code. The source code seemed very complicated at first, and I thought it'd be extremely hard to figure out the code. Here's a screenshot from the windows application.

![Screenshot](/images/blog/dot-matrix-6.jpg)

We had to integrate this with a Raspberry Pi which runs Linux. The present code was in C sharp. C sharp is properly supported only in windows. So we had to rewrite the whole code in another launguage that Linux supports if we had to get this to work. 

![Nikhil](/images/blog/dot-matrix-5.jpg)

It seemed scary at first but thanks to Nikhil. His experience at debugging complicated codes in Qualcomm was very helpful. He executed the code in Visual Studio, debugged it, understood what the packet information was. Once we looked at what byte information of the packets that we being sent into the device we were able to figure out the protocol. This was not as complicated as it seemed.

![Surya](/images/blog/dot-matrix-4.jpg)

Then we were able to program the protocol in Python. The code was less than 100 lines, and not as complicated as we assumed it would be at first. Here's an image with the dot matrix display coded with our algorithm in Python.

![Full](/images/blog/dot-matrix-3.jpg)

This is a great learning lesson for me. 

> Most of the codes that look very complicated are not so complicated once you understand the logic behind them.

From here, next time I encounter a complicated code, I will debug it, and try to figure out the logic instead of thinking that it's impossible to figure things out. 

**Thanks Nikhil!**