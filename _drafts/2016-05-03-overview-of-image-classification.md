---
layout: post
title: "An Overview Of Image Classification"
description: ""
headline: 
modified: 2016-05-03
category: Other
tags: [jekyll]
imagefeature: 
mathjax: 
chart: 
comments: true
featured: false
---

Let's consider the case when we are trying to classify an image.

To classify a new image, this is intuitive the first approach that strikes my mind. Compare the new image with all the images in the dataset and find the one it is closest to. The class of that closest image must be assigned to the new image. This method is called the nearest neighbour classifier.

But on further thinking, this doesn't seem to be ideal way to do things. What if there's a small error in one of the training images. That one image can mess up the algorithm. Hence, this is not a robust way to do it.

This method gives a 38.6% accuracy on the CIFAR-10 which is better than guessing at random (10%) but no where close to human level (94%). The best method available as of now, the Convolutional Neural Networks (CNNs) give 95%.

Hence, we go for the K nearest neighbour classifier which looks for the top k images which that are the closest, and have a vote on the labels. This makes more sense as it'll not be vulnerable like the nearest neighbour.

The computational complexity for the above methods is insanely high because we do a comparison with each and every method in the test set. If the test set has 1000 times more images, the code will also take about a thousand times more. This is ideally not how we'd like things to go.

There is a different approach that we can take here that will help resolve the problem of computational complexity. We derive a model from the test images that can be used! The model's size would be the same irrespective of the number of training images.

The simplest approach here is the linear classifier. The input will be pixels of an image and the output will be a probability function that shows how probable the test image is to each of the classes. A normal matrix transformation is used. 

Further, a better and more complicated approach is using neural neural networks are basically multiple layers of these linear classifiers put together. These can get more and more complicated, and this is what's going on in the field. Over the last five years, better networks are being discovered and there's a lot of improvement in the classification accuracy.

This is a very interesting field which is rapidly growing and there will be no shortage of jobs in the near future.