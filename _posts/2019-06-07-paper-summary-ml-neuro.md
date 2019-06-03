---
layout: post
title: "Paper Summary: ML in Systems Neuroscience"
description: ""
headline: 
modified: 2019-06-07
category: Technical
tags: [technical]
imagefeature: 
mathjax: 
chart: 
comments: true
featured: false
---

Here's my summary of the paper [The Roles of Supervised Machine Learning in Systems Neuroscience](https://www.sciencedirect.com/science/article/pii/S0301008218300856).

Well written paper, got a very good overview of the work going on in the interesection of ML (supervised) and neuroscience. I did not dive deep into all the paper quoted, I can get back to them whenever I need. Here's how I'd explain the paper to a friend. 

There are four ways in which supervised ML is be applied to neuroscience. 

### Solving engineering problems

Used when we want to find a model mapping from X to Y and all we care about is good predictive accuracy on Y. We only care about the in/out relationships, we don't want to understand the why and how of it. Example: for building descriptive models of the brain for encoding and decoding.

Applications:
* Predicting future activity in the brain. (detecting epilepsy before it happens)
* Encoding: helps create artificial human sensors (prosthetic eye)
* Decoding: helps create artificial human actuators (artificial limbs)
* Predicting diseases based on brain scans

### Identifying predictive variables

Used when we also want to explain "which variables are related/interesting?" Helps understand the brain better internally. Traditionally, simple models like linear regression were used; but studies have shown that ML performs much better which means that these simple models are unable to captures the *interesting* parts of the dataset. The image in the paper explains the most important application of these algorithms. 


### Benchmarking simple models

Helps serve as a baseline is see if the simple models are capturing all variations in the data. ML methods can serve as an upper bound on the accuracy for this data. 

### Serving as model for the brain

Comparing the visual pathway in brain and CNNs show crazy amount of similarity. [Recent work](https://science.sciencemag.org/content/364/6439/eaav9436) has shown which inputs you can provide to the brain to show maximum excitement in V4 of the brain. This is evidence that we're making some progress. You can't really say ML doesn't inform neuroscience anymore. We are able to model the brain at a higher level of abstraction using these models.

Bottom up approaches have been tried too, where biologically plausible NN architectures have been proposed in the recent years. 

### Final thoughts

I read this paper for the ML+neuro meeting at MILA. Here are a few more things I learnt there:
* Blink creates huge problems in EEG data, lots of research is going into avoiding it. It's mainly because of all the muscles involved when you do a blink.
* [Janelia farm](https://en.wikipedia.org/wiki/Janelia_Research_Campus) in the US has top research working there, they do a lot of cool work. Have lots of funds.
* Clarity regarding bias in the model (inductive bias) and bias in the dataset. They are two different problems. 