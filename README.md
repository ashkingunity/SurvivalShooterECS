# 🏹 Unity DOTS --- ECS Gameplay Prototype

A learning-focused Unity project built to explore **Data‑Oriented
Technology Stack (DOTS)** --- applying **Entity Component System (ECS)**
architecture, **Burst Compiler**, and the **C# Job System** to create
scalable, high‑performance gameplay features.

This repository demonstrates practical implementation of DOTS‑driven
gameplay patterns including movement, physics interactions, enemy
spawning, damage systems, UI sync, and hybrid workflows for animation &
audio.
------------------------------------------------------------------------

[![Game Video](https://img.youtube.com/vi/sqiy2lf-Kjs/maxresdefault.jpg)](https://youtu.be/sqiy2lf-Kjs)
### [ Survival Shooter using Unity DOTS ](https://youtu.be/sqiy2lf-Kjs)

------------------------------------------------------------------------

## 📌 Project Overview

| Category          | Tools / Framework                                 |
| ----------------- | ------------------------------------------------- |
|  Engine           | **Unity 6.3 lts (6000.0.64f1) (DOTS compatible)** |                   
|  Architecture     | **Entity Component System (ECS)**                 |        
|  Performance      | **Burst Compiler + C# Job System**                |
|  Rendering        | **Unity Graphics (Entities Graphics)**            |
|  Physics          | **Unity Physics (ECS)**                           |
|  Input            | **New Input System**                              |
|  Render pipeline  | **Universal Render Pipeline (URP)**               |

> If you are opening this project, please use a **DOTS‑compatible Unity 6.3
> LTS version 6000.3.3f1 or above**.

------------------------------------------------------------------------

## 📦 DOTS Packages Used

This project was built using the following Unity DOTS packages:

-   Entities `v1.4.4`
-   Entities Graphics `v1.4.17`
-   Unity Physics `v1.4.4`

The packages automatically installed as dependencies are:

-   Burst
-   Jobs
-   Unity Collections
-   Unity Mathematics

Hybrid workflows use standard GameObject systems for:

-   Animation
-   Audio
-   AI Navigation

------------------------------------------------------------------------

## 🎮 Gameplay Systems Implemented

### Core Systems (DOTS / ECS)

-   ECS‑driven **player movement & input**
-   Physics‑based character motion
-   Gun shooting using **raycast hit‑detection**
-   Collision & trigger event handling
-   Player & enemy **health system**
-   Enemy **spawner & lifecycle management**
-   Scalable entity creation / destruction

### Hybrid Feature Bridge (Non‑DOTS Supported Systems)

Implemented using GameObject workflow where DOTS does not yet provide
native support:

-   Animation control
-   Audio playback
-   AI navigation / movement controllers

### UI & Game State

-   UI score + health updates via ECS
-   Pause system at **Entity World context**
-   Game over state handling

------------------------------------------------------------------------

## 🧠 DOTS Concepts Practiced

This project explores:

-   `IComponentData`
-   `IBufferElementData`
-   `IEnableableComponent`
-   `SystemBase` & `ISystem`
-   `SystemAPI.Query` iteration patterns
-   `EntityCommandBufferSystem`
-   `ComponentSystemGroup` organization

Integrated execution groups:

-   `InitializationSystemGroup`
-   `SimulationSystemGroup`
-   `PhysicsSystemGroup`
------------------------------------------------------------------------

## 🧭 Roadmap / Future Improvements

-   Expanded AI behavior using ECS
-   Event‑driven gameplay messaging
-   Full ECS‑based animation bridge
-   Performance metrics & profiling

------------------------------------------------------------------------

## 🙌 Learning Outcomes

Working on this project strengthened my understanding of:

-   Data‑oriented programming
-   Multithreaded gameplay design
-   Memory‑efficient system modeling
-   Scalable gameplay architecture

DOTS fundamentally changed how I think about **performance‑first game
development** --- and this project represents my ongoing exploration of
ECS‑driven workflows.

------------------------------------------------------------------------

## 🤝 Feedback & Collaboration

If you're experimenting with DOTS or building ECS gameplay systems ---
I'd love to connect and exchange ideas.

Feel free to open an issue or share suggestions!

------------------------------------------------------------------------
