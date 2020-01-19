---
layout: page
permalink: /about/index.html
title: Surya - About
tags: 
imagefeature: fourseasons.jpg
chart: true
---

{% assign total_words = 0 %}
{% assign total_readtime = 0 %}
{% assign featuredcount = 0 %}
{% assign statuscount = 0 %}

{% for post in site.posts %}
    {% assign post_words = post.content | strip_html | number_of_words %}
    {% assign readtime = post_words | append: '.0' | divided_by:200 %}
    {% assign total_words = total_words | plus: post_words %}
    {% assign total_readtime = total_readtime | plus: readtime %}
    {% if post.featured %}
    {% assign featuredcount = featuredcount | plus: 1 %}
    {% endif %}
{% endfor %}

My name is Surya. I'm presently doing my MSc Computer Science at McGill University and [Mila](http://mila.quebec/). I work on ideas at the intersection of reinforcement learning and neuroscience with [Blake Richards](http://linclab.org/). I'm interested in helping the research community collectively figure out the general principles behind intelligence. 

I've been collaborating with researchers at several institutes including RWTH Aachen, IISc Banglore, Defense Research Development Organisation and NIT Warangal. 

## Here are some of my recent talks:
* 17th Jan 2019: Gave a talk on the paper "A deep learning framework for neuroscience" at Mila (Neural AI reading group) -- with Yoshua Bengio and Blake Richards. ([slides](https://docs.google.com/presentation/d/1F3f2bTNeuPT1uT_ckymA3AYA5elydbQWU6fvwnNdqp0/edit#slide=id.p)) (video will be up soon)
* 13th Sept 2019: Gave a talk on the paper "The Successor Representation: Its Computational Logic and Neural Substrates" at Mila (Neural AI reading group). ([slides](https://docs.google.com/presentation/d/1H0CC2TK3Uu8r6gqjh8PqJABAidNBR5GKpfl47Eu4sN0)) ([video](https://www.youtube.com/watch?v=DAaQsrSTusg))
* 12th Aug 2019: Gave a talk on the paper "What does it mean to understand a neural network" at Mila (Cognitive AI reading group). ([slides](https://docs.google.com/presentation/d/1DG-Zij5s5jRe3-Tx4Klbgn1uJyqkKLrsLDAKEU-f1DM/edit)) ([video](https://www.youtube.com/watch?v=TQU6bx5k_4U))
* 15th July 2019: Gave a talk on the paper "Reinforcement learning, fast and slow" at Mila (Cognitive AI reading group). ([slides](https://docs.google.com/presentation/d/1IGxN03ifKGwHUiqhsHjw106ieEgDCwGcenotL39-LM0/edit)) ([video](https://www.youtube.com/watch?v=JL_SSDCo4jw))
* 10th June 2019: Gave a talk on the paper "Modeling the brain and dendritic solutions to the credit assignment problem" at Mila (Cognitive AI reading group). ([slides](https://docs.google.com/presentation/d/1nD_AHxUT0OXQd_EIxy5I9rnmwdFiqjx96p5_LMOJkrE/edit)) ([video](https://www.youtube.com/watch?v=Aq6sjrKgeH4))


## New year resolutions and related updates:
* My resolutions for 2020 

## My initiatives:
* Organisation - [Game Automators](http://gameautomators.com/)
* YouTube Channel - [The Motivated Engineer](https://www.youtube.com/user/suryapenmetsa) (2.4 million views, 11.2 thousand subscribers)
* YouTube Channel - [My Viewpoint](https://www.youtube.com/channel/UCVlGviwwdZuA75bFQ7y8XZg/) (65 thousand views, 660 subscribers)
* Podcast - [Thinking Out Loud](https://www.youtube.com/playlist?list=PLDC3KYAAKOWui8sAsHm2GTKHZK0-fLjOc)
* Podcast - [Students Productivity](http://studentsproductivity.com/)

## Friends and collaborators
* [Sudheesh Singanamalla](https://sudheesh.info/)
* [Amarjot Singh](https://www.amarjotsingh.com/)


I make educational videos on the following YouTube channels. 

Name: The Motivated Engineer
About: This channel has tutorials and demonstrations of various electronics and image processing projects. It also has videos on interesting facts.
Subscribers: 6000
Views: 1,500,000


Name: My Viewpoint
About: These are short informative videos on various topics through smooth animations.

Subscribers: 500

Views: 20,000