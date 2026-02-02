# XRiddle

XRiddle is an experimental Augmented Reality (AR) project built in Unity that explores how **multiple AR frameworks can coexist within a single application**.

The project investigates the use of **Vuforia (image tracking)** and **Unity AR Foundation (plane tracking)** as **mutually exclusive AR pipelines**, dynamically enabling and disabling each system based on the active interaction mode.

---

## Project Goals

- Explore architectural patterns for using **multiple AR SDKs** in one Unity project
- Avoid camera, sensor, and lifecycle conflicts between AR systems
- Build a clean, mode-based AR experience suitable for research and prototyping
- Document practical limitations and lessons learned

---

## AR System Architecture

XRiddle uses two AR frameworks that operate **independently**:

- **Vuforia Engine**  
  - Used for image tracking–based interactions  
  - Disabled when plane tracking is active  

- **Unity AR Foundation**  
  - Used for plane detection and spatial placement  
  - Disabled when image tracking is active  

Only **one AR system is active at runtime** to ensure stability, performance, and predictable behavior.

This approach mirrors real-world XR applications where different AR modes are isolated rather than run simultaneously.

---

## Built With

- **Unity**
- **C#**
- **Vuforia Engine**
- **Unity AR Foundation**

---

## Forking

Feel free to fork and experiment with this project.  
If you reuse or publish derived work, please credit:

> XRiddle by Mesbah Ur Rahman
