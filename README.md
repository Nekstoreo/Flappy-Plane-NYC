# **Flappy Plane NYC ✈️🏙️**

<div align="center">
  <img src="https://github.com/user-attachments/assets/24b391ff-d220-4e92-ad39-3e6d868124c7" alt="logo-android" width="200"/>
  
  **Un emocionante juego endless flyer ambientado en Nueva York**
  
  [![Unity](https://img.shields.io/badge/Unity-2022.3.62f1-000000?style=for-the-badge&logo=unity&logoColor=white)](https://unity.com/)
  [![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/csharp/)
  [![Android](https://img.shields.io/badge/Android-3DDC84?style=for-the-badge&logo=android&logoColor=white)](https://developer.android.com/)
  [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg?style=for-the-badge)](https://opensource.org/licenses/MIT)
</div>

---

## 📖 **Descripción del Juego**

🎮 **Flappy Plane NYC** es un emocionante videojuego 2D de tipo "endless flyer" para Android. Controla un avión tocando la pantalla para ascender y navega a través de huecos entre torres en un entorno inspirado en Nueva York.

� **Características destacadas:**
- ✨ Parallax scrolling con fondos de NYC
- 🌅 Transiciones dinámicas del cielo (día → tarde → noche)
- 🏢 Navegación entre torres de rascacielos
- 🎯 Sistema de puntuación progresivo
- 💥 Efectos visuales y sonoros inmersivos
- 📱 Controles táctiles intuitivos

---

## 🛠️ **Stack Tecnológico**

| Tecnología | Versión | Propósito |
|------------|---------|-----------|
| ![Unity](https://img.shields.io/badge/Unity-000000?style=flat&logo=unity&logoColor=white) | **2022.3.62f1 LTS** | Motor de juego principal |
| ![C#](https://img.shields.io/badge/C%23-239120?style=flat&logo=c-sharp&logoColor=white) | **10.0+** | Lenguaje de programación |
| ![Android](https://img.shields.io/badge/Android-3DDC84?style=flat&logo=android&logoColor=white) | **API 21+** | Plataforma objetivo |
| ![Visual Studio](https://img.shields.io/badge/Visual%20Studio-5C2D91?style=flat&logo=visual-studio&logoColor=white) | **2022** | IDE de desarrollo |

### 📚 **Sistemas y Librerías Unity**
- 🎯 **Unity 2D Physics** - Sistema de física para colisiones y movimiento
- 🔊 **Unity Audio System** - Efectos de sonido y música de fondo
- 🖼️ **Unity UI (uGUI)** - Interfaz de usuario y menús
- 🎬 **Unity Animation System** - Animaciones de sprites y transiciones
- 📝 **TextMeshPro v3.0.7** - Renderizado de texto avanzado
- 🎮 **Unity Input System** - Manejo de controles táctiles

### 🧩 **Arquitectura del Código**
- 🎮 **GameManager** - Control central del estado del juego
- ✈️ **PlaneController** - Lógica de movimiento y física del avión
- 🏗️ **TowerSpawner** - Generación procedural de obstáculos
- 🌅 **SkyTransition** - Sistema de transiciones día/noche
- 📱 **UI Management** - Gestión de interfaces y puntuación

## ✨ **Características Principales**

🎮 **Mecánicas de Juego:**
* 📱 **Control Intuitivo** - Toca la pantalla para hacer que el avión ascienda y esquiva las torres
* �️ **Obstáculos Procedurales** - Torres generadas aleatoriamente para máxima rejugabilidad
* � **Sistema de Puntuación** - Compite por la puntuación más alta y supera tus récords
* ⚠️ **Físicas Realistas** - Gravedad y movimiento natural del avión

� **Entorno Visual:**
* 🌅 **Transiciones Dinámicas** - Ciclo día/tarde/noche que cambia automáticamente
* �️ **Parallax Scrolling** - Fondos multicapa de Nueva York con profundidad visual
* ☁️ **UI Integrada** - Interfaz flotante sobre capas de nubes
* 🎨 **Assets Personalizados** - Gráficos únicos y diseño visual coherente

🔧 **Funcionalidades Técnicas:**
* ⏸️ **Sistema de Pausa** - Pausa y reanuda el juego cuando necesites
* 🔄 **Reinicio Rápido** - Reinicia instantáneamente para intentar superar tu récord
* 💥 **Efectos Visuales** - Explosiones y animaciones fluidas
* 🔊 **Audio Inmersivo** - Efectos sonoros y música de ambiente

## 🎮 **Cómo Jugar**

1. ✈️ **Toca para Volar** - Toca la pantalla para que el avión dé un pequeño impulso hacia arriba
2. 🏗️ **Esquiva las Torres** - Navega el avión a través de los huecos entre las torres
3. 🎯 **Acumula Puntos** - Gana un punto por cada par de torres que superes con éxito  
4. ⚠️ **¡No Choques!** - Si el avión toca una torre, el juego termina
5. 🔄 **Intenta de Nuevo** - Reinicia la partida para mejorar tu puntuación y superar tu récord

## 🚀 **Instalación y Configuración**

### 📋 **Prerrequisitos**
- ![Unity](https://img.shields.io/badge/Unity-2022.3.62f1_LTS-000000?style=flat&logo=unity) Unity 2022.3.62f1 LTS o superior
- ![Android SDK](https://img.shields.io/badge/Android_SDK-API_21+-3DDC84?style=flat&logo=android) Android SDK (API level 21 o superior)
- ![IDE](https://img.shields.io/badge/IDE-VS_2022_or_VS_Code-5C2D91?style=flat&logo=visual-studio) Visual Studio 2022 o Visual Studio Code

### ⚡ **Pasos de Instalación**
1. **Clona el repositorio:**
   ```bash
   git clone https://github.com/tu-usuario/Flappy-Plane-NYC.git
   cd Flappy-Plane-NYC
   ```

2. **Configura Unity:**
   - Abre Unity Hub
   - Añade el proyecto desde la carpeta clonada
   - Asegúrate de tener Unity 2022.3.62f1 LTS instalado

3. **Configuración Android:**
   - Ve a `File > Build Settings`
   - Selecciona `Android` como plataforma objetivo
   - Configura el Android SDK path en `Edit > Preferences > External Tools`

4. **¡Compila y juega!**
   - Presiona `Ctrl+B` para compilar
   - O presiona `Play` en el editor para probar

## 📁 **Estructura del Proyecto**

```
📂 Flappy-Plane-NYC/
├── 📁 Assets/
│   ├── 🎬 Animations/          # Animaciones de sprites y transiciones
│   ├── 🔊 Audio/              # Efectos de sonido y música de fondo
│   ├── 🖼️ Sprites/           # Texturas, fondos y sprites del juego
│   ├── 📜 Scripts/            # Código C# principal del juego
│   │   ├── GameManager.cs     # Controlador principal del juego
│   │   ├── PlaneController.cs # Física y controles del avión
│   │   ├── TowerSpawner.cs    # Generación de obstáculos
│   │   └── SkyTransition.cs   # Sistema día/noche
│   ├── 🎭 Prefabs/           # GameObjects prefabricados
│   ├── 🎪 Scenes/            # Escenas principales de Unity
│   └── 🅰️ Fonts/            # Fuentes tipográficas (TextMeshPro)
├── 📁 ProjectSettings/        # Configuración del proyecto Unity
├── 📁 Packages/              # Dependencias y paquetes Unity
└── 📄 README.md              # Este archivo
```

---

## 📊 **Estadísticas del Proyecto**

| Métrica | Valor |
|---------|-------|
| 📁 **Líneas de Código** | ~500+ líneas C# |
| 🎯 **Scripts Principales** | 6 scripts core |
| 🎨 **Assets Visuales** | Sprites personalizados NYC |
| 🔊 **Audio** | SFX + música ambiente |
| 📱 **Plataforma** | Android (escalable) |
| ⚡ **Rendimiento** | 60 FPS objetivo |

---

## 🤝 **Contribuir**

¡Las contribuciones son bienvenidas! Si quieres contribuir a este proyecto:

1. 🍴 Fork el proyecto
2. 🌟 Crea tu Feature Branch (`git checkout -b feature/AmazingFeature`)
3. 💾 Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. 📤 Push a la Branch (`git push origin feature/AmazingFeature`)
5. 🔀 Abre un Pull Request

Por el momento, este es un proyecto personal. Sin embargo, se agradecen sugerencias y comentarios.

## 📝 **Licencia**

📄 Este proyecto está bajo la Licencia MIT. Consulta el archivo [LICENSE](LICENSE) para más detalles.

---

<div align="center">
  
  **¡Disfruta volando sobre los cielos de Nueva York! ✈️🗽**
  
  Hecho con ❤️ y Unity
  
</div>
