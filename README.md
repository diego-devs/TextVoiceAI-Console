# TextVoiceAI

Este es un proyecto de consola escrito con .NET 7 y C# para probar el uso de Azure Cognitive Services y sus utilidades en sintetizador de texto a voz y reconocimiento de voz a texto.

Para usar el servicio de Amazon Cognitive Services primero es necesario instalar el paquete de nuget Microsoft.CognitiveServices.Speech

Tambien es necesario configurar dos variables de entorno usando el comando: 

```bash
setx SPEECH_KEY <key>
setx SPEECH_REGION <region> 
```

Verifica que la region sea la misma que esta configurada en el dashboard de Azure Cognitive Services. En mi caso es: eastus.

Para ejecutar el programa usa: 

```bash
dotnet run
```
