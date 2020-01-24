
Thank you for purchasing Fluid Dynamics!

Start Guide 
 
1. Create new Plane. Change rotation to 90 0 -180 in Transform component.(To fit 2D view). Change scale X and Z values as much as you need in Transform component.
2. Add Main_Fluid_Simulation component
3. Create new Gameobject
4. Add any of Emitters(Component/FluidDynamics/Emitters)
5. Make sure that all emitters are in Fluid Plane bounds.
6. Enjoy.

Main_Fluid_Simulation:
 
Core of Fluid Dynamics, it is in charge of simulating the flow of the fluid. Under the hood this component stores the velocity of the flow at every point inside the simulation area and every frame will update them following a set of equations that describe how the flow should behave. This component also represents the visuals of the system. It contains all the particles you can see when running the simulation. This component will use the velocity data to update the particles’ position every frame.  
 
SIMULATE - Set to true to start the simulation. 
QUALITY - The resolution of simulation, this translates to how many velocity vectors are used for the simulation. The higher the quality the better it will look, but the slower it will perform. 
VORTICITY - The higher the vorticity the more local swirls and spins the simulation will have.
VISCOSITY - The viscosity represents the “thickeness” of the material you are simulating. 
CACHE VELOCITY - (Advanced) Will get a copy of the velocity data in the CPU every frame. Useful for accessing the data from other components (for example, Fluid Follower). Only use when needed as it has an impact on the performance of the system. 
SIMULATION QUALITY - (Advanced) Represents how realistic is the simulation. A higher number is more realistic but will take up more processing power to simulate. 
SIMULATION SPEED - (Advanced) The speed of the simulation. 
VELOCITY DISSIPATION - (Advanced) Specifies how long the velocity in the simulation lasts before it starts to “decay” and loose its value. The lower the number the faster it “decays”, a one (1) means it will never dissipate. 
 
COLOUR GRADIENT - The colour and opacity of the particles. Depending on the “strength” or density of the particles at any given point you can choose a colour and opacity using this Colour Gradient. 
UPDATE GRADIENT - If true, will apply the gradient to the particles again every frame. Only use when changing the values at runtime (for example, inside the editor while adjusting the colours). 
CACHE PARTICLES - Will get a copy of the particles data in the CPU every frame. Useful for accessing this data from other components. Only use when really needed as it has an impact on the performance of the system. 
AREA RESOLUTION - The resolution of the particle area can also be thought of as how many particles the system allows. Higher values will make for a high definition looking fluid, lower values will look more “pixelated”. 
PARTICLE LIFE - How long does a particular particle live. The higher the value the longer it will last, a value of one (1) means the particle will stay alive forever.

Emitters:
 
Fluid_Dynamics_Mouse_Emitter component lets you add particles to a Particle Area using the left mouse button and modify the velocities of the simulation with the right mouse button (the direction of the velocity will be changed to follow the movement of the mouse). 
 
ALWAYS ON - If true, you don’t need to press any buttons to start modifying the velocities or the particles. Moving the mouse over the simulation will be the same as if the player would be pressing both the left and right mouse buttons. 
PARTICLES STRENGTH - How much particles to add for every frame the left mouse button is pressed. 
PARTICLES RADIUS - The radius of the area where the particles will be added.
VELOCITY STRENGTH - The speed of the velocity you are modifying. 
VELOCITY RADIUS - The radius of the area where the velocity will be modified. 
 
Fluid_Dynamics_Particles_Emitter - This component allows you to add particles to a particle area.  
 
STRENGTH - How much particles to add for every frame this component is activated. 
SET SIZE MANUALLY - If true, the size of the area to modify is set manually (see Radius). If false, the size will be set by the Scale of the Game Object that has this component. RADIUS - (Only if Set Size Manually is true) The radius of the area to modify. 
 
 
Fluid_Dynamics_Velocity_Emitter - allows you to modify the velocities in the fluid simulation by changing their direction and magnitude.  
SPEED - The speed of the velocity you are modifying. 
DIRECTION - Global Rotation: The direction of the velocity that is being added will be given by the rotation of the Game Object.  
Movement Direction: The direction of the velocity that is being added will be given by the direction of movement of the Game Object. Useful for objects that want to modify the velocity as they move along the fluid. 
SET SIZE MANUALLY - If true, the size of the area to modify is set manually (see Radius). If false, the size will be set by the Scale of the Game Object that has this component. RADIUS - (Only if Set Size Manually is true) The radius of the area to modify. 
 
Fluid_Dynamics_Circle_Obstacle - defines exclusion areas within the simulation where the fluid is not allowed to enter. This is useful for making the simulation go around geometries or meshes in your level.  
 
SET SIZE MANUALLY - If true, the size of the area to block is set manually (see Radius). If false, the Scale of the Game Object that has this component will be used instead. RADIUS - (Only if Set Size Manually is true) The radius of the area to block. 
 
Fluid_Dynamics_Polygon_Obstacle/Fluid_Dynamics_Edge_Obstacle uses a Polygon/Edge Collider 2D to define exclusion areas within the simulation where the fluid is not allowed to enter. This is useful for making the simulation go around geometries or meshes in your level. 
 
Fluid_Dynamics_Flow_Follow - This component can be attached to any game object and when placed within the simulation area it will follow the flow of the fluid.

If you have any questions please contact me via email - seedounitydev@gmail.com