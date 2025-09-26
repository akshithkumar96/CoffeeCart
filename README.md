# Coffee Cart Unity Game

A Unity-based coffee cart management game where players collect coffee beans, process them into coffee, and serve customers to earn money.

## Game Features

- **Player Controller**: Smooth 2D movement with WASD/Arrow keys
- **Resource Management**: Collect coffee beans from an infinite supply area
- **Bean Stacking**: Carry up to 3 bags of beans at once with visual stacking
- **Coffee Processing**: Use coffee machine to process beans into coffee cups
- **Customer AI**: Serve customers who wait at designated counters
- **Scoring System**: Earn money by serving customers
- **Visual Feedback**: Animated coin rewards and progress indicators

## Architecture & SOLID Principles

This project follows SOLID principles and clean architecture:

### Single Responsibility Principle (SRP)
- Each class has a single, well-defined responsibility
- `PlayerController` handles only player movement and inventory
- `CoffeeMachine` manages only coffee processing
- `Customer` handles only customer behavior

### Open/Closed Principle (OCP)
- Interfaces like `IInteractable` and `ICollectible` allow extension without modification
- New interactable objects can be added without changing existing code

### Liskov Substitution Principle (LSP)
- All implementations of interfaces can be substituted without breaking functionality
- `CoffeeBean` and `CoffeeCup` both implement `ICollectible` correctly

### Interface Segregation Principle (ISP)
- Interfaces are focused and specific (`IInteractable`, `ICollectible`, `IResourceProvider`)
- Classes only depend on methods they actually use

### Dependency Inversion Principle (DIP)
- High-level modules depend on abstractions (interfaces)
- Low-level modules implement these abstractions

## Project Structure

```
Assets/Scripts/
├── Interfaces/           # Interface definitions
│   ├── IInteractable.cs
│   ├── ICollectible.cs
│   └── IResourceProvider.cs
├── Data/                 # Data containers
│   └── GameData.cs
├── Enums/                # Enumeration types
│   ├── GameState.cs
│   └── ItemType.cs
├── Player/               # Player-related scripts
│   └── PlayerController.cs
├── Items/                # Item scripts
│   ├── CoffeeBean.cs
│   └── CoffeeCup.cs
├── Resource/             # Resource management
│   └── ResourceArea.cs
├── Machines/             # Machine scripts
│   └── CoffeeMachine.cs
├── Customer/             # Customer AI
│   ├── Customer.cs
│   └── CustomerManager.cs
├── UI/                   # User interface
│   └── UIManager.cs
├── Managers/             # Game management
│   └── GameManager.cs
├── Utilities/            # Utility scripts
│   ├── ObjectPool.cs
│   └── GameObjectExtensions.cs
└── Setup/                # Setup and configuration
    └── GameSetup.cs
```

## Setup Instructions

### 1. Unity Project Setup
1. Create a new 2D Unity project
2. Import all scripts into the `Assets/Scripts/` folder
3. Create the following folder structure:
   - `Assets/Prefabs/`
   - `Assets/Materials/`
   - `Assets/Scenes/`

### 2. Scene Setup
1. Create a new scene
2. Add an empty GameObject and attach the `GameSetup` script
3. Configure the setup references in the inspector
4. Run the setup or use the context menu "Setup Game"

### 3. Prefab Creation

#### Player Prefab
1. Create a 2D Sprite GameObject
2. Add `Rigidbody2D` component
3. Add `Collider2D` component
4. Add `PlayerController` script
5. Create a child GameObject for bean stacking
6. Save as prefab

#### Coffee Bean Prefab
1. Create a 2D Sprite GameObject (brown cube)
2. Add `CoffeeBean` script
3. Add `Collider2D` component (trigger)
4. Save as prefab

#### Coffee Cup Prefab
1. Create a 2D Sprite GameObject (white cylinder)
2. Add `CoffeeCup` script
3. Add `Collider2D` component (trigger)
4. Save as prefab

#### Customer Prefab
1. Create a 2D Sprite GameObject (capsule)
2. Add `Customer` script
3. Add `Collider2D` component (trigger)
4. Create child objects for visual feedback
5. Save as prefab

### 4. UI Setup
1. Create a Canvas
2. Add TextMeshPro components for score and bean count
3. Create a coin prefab for animations
4. Configure UI references in managers

### 5. Layer Setup
1. Create layers: "Interactable", "Player", "Items"
2. Assign appropriate layers to game objects
3. Configure layer masks in scripts

## Controls

- **WASD** or **Arrow Keys**: Move player
- **E**: Interact with objects (collect beans, use machine, serve customers)
- **Escape**: Pause/Resume game

## Game Flow

1. **Collect Beans**: Move to the green resource area and press E to collect coffee beans
2. **Process Coffee**: Take beans to the coffee machine and press E to start processing
3. **Collect Coffee**: Once processing is complete, press E to collect the coffee cup
4. **Serve Customers**: Take coffee to waiting customers and press E to serve them
5. **Earn Money**: Customers pay you and disappear, new customers will appear

## Customization

### Game Data
Modify the `GameData` ScriptableObject to adjust:
- Player movement speed
- Maximum bean stack size
- Coffee processing time
- Customer spawn delay
- UI animation settings

### Visual Customization
- Replace primitive shapes with proper sprites
- Add particle effects for interactions
- Implement smooth animations
- Add sound effects and music

## Performance Considerations

- Object pooling for frequently spawned objects (beans, coins)
- Efficient collision detection using layer masks
- Minimal Update() calls with event-driven architecture
- Proper memory management with object references

## Extensibility

The modular design allows for easy addition of:
- New item types
- Different machine types
- Various customer behaviors
- Power-ups and upgrades
- Multiple levels or maps

## Troubleshooting

### Common Issues
1. **Player not moving**: Check Rigidbody2D settings and input axes
2. **Interaction not working**: Verify layer masks and collider settings
3. **UI not updating**: Ensure proper references in managers
4. **Customers not spawning**: Check CustomerManager configuration

### Debug Features
- Gizmos show interaction ranges
- Console logs for important events
- Inspector values for runtime debugging

## Future Enhancements

- Save/Load system
- Multiple difficulty levels
- Achievement system
- Sound and music integration
- Mobile touch controls
- Multiplayer support
- Advanced customer AI with preferences
- Shop system for upgrades
- Multiple coffee types and recipes
