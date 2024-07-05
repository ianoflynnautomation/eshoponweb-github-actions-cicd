# CI Pipeline for Building and Testing a Monolithic application

When working with a monolithic application there can be a lot of projects and test projects that all need to be built and tested.
When working with a large team where engineers are pushing changes regularly and code is merge continuously its important that the 
ci pipeline gives the engineers fast feedback and new code is tested as fast as possible before an engineer can merge changes.


## Overview: 
1. Build solution, test projects and web assets in separate jobs in parallel
2. Publish pipeline artifacts to be used for test jobs
3. Run all tests in separate jobs in parallel
4. TODO: build and push docker files to docker

## We want to go fast! parallel everything!

We build test projects in parallel using project pattern. \
For example: below we build all unit test projects together.

![image](https://github.com/ianoflynnautomation/eshoponweb-github-actions-cicd/assets/68143624/4397bdde-94bb-4855-8705-2a5fd620b980)

We do this for unit, integration and functional test projects. Depending on the number of agents you have, this can be set to allow maximum number of agents to be in use to run jobs. /
In this example as github gives you 6 free agents we build tests by test type.

![image](https://github.com/ianoflynnautomation/eshoponweb-github-actions-cicd/assets/68143624/602108f0-5aaa-4e13-8983-f977e00976b5)

We do the same for running tests. 

