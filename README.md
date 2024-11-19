# What we will be doing

![alt text](https://learn.microsoft.com/en-us/azure/devops/pipelines/architectures/media/azure-devops-ci-cd-architecture.svg?view=azure-devops#lightbox)


## Table of Contents

- [Testing Strategies](#testing-strategies)
  - [Unit Tests](#unit-tests)
  - [Integration Tests](#integration-tests)
  - [End-to-End (E2E) Tests](#end-to-end-e2e-tests)
- [CI/CD Pipeline](#cicd-pipeline)
  - [Continuous Integration](#continuous-integration)
  - [Continuous Deployment](#continuous-deployment)


# Testing Strategies
The project employs multiple additional testing strategies for high coverage and robust quality control:

## BUnit Unit Testing 
 BUnit tests are located in the  tests/Blazor.UI.UnitTests directory


## Integration Testing 

 ## End-to-End (E2E) Tests
 This repo gives multiple of examples and techniques to running UI automated tests using Playwright.

  1. Playwright using WebApplicationFactory\
    Path: ../tests/InMemorySystemTests

   2. Playwright using Test Containers and WebApplicationFactory\
     Path: ../tests/TestContainersSystemTests

3. Playwright using Docker \
      Path: ../tests/DockerSystemTests __**In Progress__

4. Playwright using Azure Playwright Testing \
     Path: __**In Progress__

 5. Playwright using TypeScript \
      Path: ../tests/e2e __**In Progress__


# CI/CD Pipeline

## Continious Integration (CI) __**In Progress__

### Overview 

The build-and-test.yml workflow is a Continuous Integration (CI) pipeline designed to automate building, testing, and publishing artifacts for a .NET project. It utilizes GitHub Actions to streamline the CI/CD process, covering various testing stages from unit to system tests. This pipeline is triggered on pushes to the main branch and ensures consistent, repeatable build and test processes for the project.

Workflow Summary
The workflow consists of several jobs, each targeting specific stages of the CI process:

1. Build Solution: Compiles the .NET solution.
2. Build Web Assets: Builds frontend or web assets required by the application.
3. Build Tests: Compiles test projects, including unit, integration, functional, and system tests in parallell.
4. Run Tests: Executes various tests (unit, integration, functional) in parallell.
5. Run System Tests: Executes system tests in different browser environments.
6. Publish Service Artifacts: Publishes the artifacts as Docker images, enabling deployment

## Continuous Deployment (CD) __**In Progress__


