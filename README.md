# WindmillTask
assignment for windmill studio

For the node states, I used a state pattern as an architectural choice that promotes reusability, modularity, and maintainability. I used events that will notify other parts of the code, with other responsibilities to update the scene depending on the state, like changing color and state by the node controller or updating the UI by the UI manager.

For the user interface, I created a Minimap using a GameObject child in the node's prefab that has a different layer and will be rendered by a different camera. I anchored it to the left-top side of the screen. In addition, I used a Mask to give it a slightly better look. You can interact with the nodes using the left mouse button to trigger transitions.

I added comments to most parts of the code. In the parts where I didn't use them, the naming is already self-explanatory.
