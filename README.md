# Pople

An online multiplayer brawler where every character is a physics-driven active ragdoll. Hit someone hard enough with a spear and their arm comes off.

Built with **Unity 6** and **Netcode for GameObjects 2.2**.

<!-- Add a short gameplay GIF here: ![Pople gameplay](docs/pople.gif) -->

## How the ragdolls work

Every character has two skeletons:

- an **animated rig** that plays the walk cycle and is never shown,
- a **physical body** made of Rigidbodies connected with ConfigurableJoints.

`CopyMotion` runs in `FixedUpdate` and sets each joint's `targetRotation` to the rotation of the matching bone in the animated rig. Mirrored limbs use the inverse rotation. Movement is done by pushing the hips with `AddForce`, so balance, stumbling and falling all come from the physics instead of from animation clips.

## Losing limbs

- The `Spear` follows the hand bone. When it touches a body part, it clears `connectedBody` on that part's joints (listed in `ConnectRig`) and the limb drops off.
- `BubbleManager` frees every axis on joints that lost their connection, so detached parts fall naturally.
- If the hips are gone, `PlayerController` hands control to the next part that is still attached: stomach, then torso, then head. You can keep fighting with what is left.

## Networking

- Netcode for GameObjects 2.2 with Unity Multiplayer Services for creating and joining sessions.
- `ClientNetworkTransform` and `ClientNetworkAnimator` keep movement owner-authoritative, so your own character responds right away.
- On spawn, non-owners have their camera, camera controller and AudioListener disabled. Only the owner reads input.
- Tested locally with Multiplayer Play Mode.

## Running it

1. Open the project with **Unity 6000.0.30f1**.
2. Link it to a Unity Cloud project (Edit > Project Settings > Services) so Multiplayer Services can sign in.
3. Open `Assets/Scenes/Game.unity`.
4. Open Window > Multiplayer > Multiplayer Play Mode, enable a second player and press Play.

## Main scripts

| Script | What it does |
| --- | --- |
| `PlayerController` | Owner input, ground check, jumping, force-based movement, falls back to the next body part when one is lost |
| `CopyMotion` | Makes each physical joint follow the animated rig |
| `ConnectRig` | Lists the joints attached to a body part |
| `Spear`, `Shield`, `Helmet` | Equipment that follows a bone; the spear breaks joints on hit |
| `BubbleManager` | Frees joints that have lost their connected body |
| `ClientNetworkTransform`, `ClientNetworkAnimator` | Owner-authoritative sync |
| `CameraController`, `UIManager` | Camera, pause menu, create/join panels |

## Tech

Unity 6 · URP 17 · Netcode for GameObjects 2.2 · Unity Multiplayer Services · Multiplayer Play Mode · Cinemachine 3 · Input System
