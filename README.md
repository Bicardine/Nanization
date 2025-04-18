# Nanization
> Simple Naninovel localization without boilerplate code. Works like a ninja!

## Features ✨
- No need to worry about `Engine.Initialized`!
- No need to get `Engine.GetService<ITextManager>()`!
- No need to worry about `DocumentLoader.IsLoaded`!
- Auto-subscribers for locale changes!
- Just use it - Nanization handles everything behind the scenes!

## Requirements 📦
- Naninovel v1.20 or later

## Installation ⚙️
Install via git or just download .unitypackage from
[Releases](https://github.com/Bicardine/Nanization/releases)

## Usage 🏹
There is many mode-ways to use Nanization!

### 1. Zen-mode (String Extensions)
- Using string extensions
- Will not auto subscribe to locale changed

```csharp
public async UniTask Localize()
{
  // Will localize your string. Even Engine.Initialized == false will wait for it and return localized string.
  myTmpText.text = await "DefaultUI.ControlPanel.Title".LocalizeAsync()
  // If there is no arguments, will localize string like whole localization path
  // (Document = "DefaultUI", Key = "ControlPanel.Title" (all after first auto exclusive dot)
}
```

Another way:
```csharp
  // (Document = "DefaultUI", Key = "ControlPanel.Title")
  myTmpText.text = await "ControlPanel.Title".LocalizeAsync("DefaultUI")
```

Of course it's work with variables string:
```csharp
var myFullLocalizedPathString = "MyCustomDocument.Key1"
// (Document = "MyCustomDocument", Key = "Key1")
myTmpText.text = await myFullLocalizedPathString.LocalizeAsync();
```


### 2. Ninja-mode
- Auto update (get current localization from locale) on locale changed
In this mod, you need to specify which document and key you want to sign and which Action<string> to call to localize

Use:
```csharp
Nanization.Subscribe("MyDocumentName", "MyKeyName", localizedValue =>
  {
      tmpText.text = localizedValue;
  });
```
- Will auto set tmpText.text every time on localization changed.
- Will localize now on subscribe, if you don't want it, set localizeNow = false
- Also have fallback as not required parameter


It's use Action, so you can use another style as well (set methods):
```csharp
private void Start()
{
    // Set OnLocalized method as method on every time locale was changed.
    Nanization.Subscribe(_documentName, _key, OnLocalized, fallback: "There is no translate!" localizeNow: false);
}

// Auto called every time on localized changed.
private void OnLocalized(string localizedValue)
{
    UnityEngine.Debug.Log($"I was localized!: {localizedValue}")
    _tmpText.SetText(localizedValue);
}
```

Not required localizeNow parameter by default is true, which mean invoke localization Action right on Subscribe().

You can also "pause" your subscriber, so...

```csharp
// Localize and subscribe to locale changed.
var nanizatoinSubscriber = Nanization.Subscribe(_documentName, _key, OnLocalized

nanizatoinSubscriber.Pause() // Will unsubscribe from OnLocaleChanged events and will not execute localizatoin callbacks so
// OnLocalized() Will not execute on locale changed becouse of Pause(), until Resume()

nanizatoinSubscriber.Resume() // Will continue execute callback on localiation changed
// Not required parameter localizeNow = true by default, which means every Resume() will invoke your action.
```

to localize it with your hand, use 
```csharp
nanizationSubscriber.Localize()
```
If your want control localization invoke it for some reason (if was seted localizeNow = false on subscribe for example)


If you don't want use nanizatoinSubscriber anymore:
```csharp
private void OnDestroy()
{
  nanizationSubscriber.Unsubscribe() // when object will destroy, invoke IDisposable
}
```


3. Bind-mode (build your own way, as ninja with method chaining!)
- If need to transfer the localization bind to runtime or perform actions depending on conditions
- If need to get info about localization (such as document, key, fallback)
- Fluent builder mode with compiler grant (strict-order)

```csharp
Nanization.Bind() // Will start bind your localization.
  .WithKey("TitileMenu.Title") // Will set key = "TitileMenu.Title"
  .WithDocument("DefaultUI") // Will set document = "DefaultUI"
  .SetFallback("Title") // Will set fallback = "Title"
  .LocalizeAsync(); // Will execute localization right now.
```

Compiler grant means you can use methods as .SetFallback() in any order in bind:

```csharp
Nanization.Bind()
  .SetFallback("Title")// Will start bind your localization.
  .WithKey("TitileMenu.Title") // Will set key = "TitileMenu.Title"
  .WithDocument("DefaultUI")
```
But you can't use SetFallback twice, or another one-time methods.
It's means IDE also will safely show field that already was setting.

For example:
```csharp
Nanization.Bind().WIthKey("SomeKey).Key // IDE will show "Key" as possible public field, and allow to access to it.
Nanization.Bind().Key // Not showing in IDE, the key wasn't setting yet.
```

It's also mean IDE will not show you .LocalizeAsync() if there is no key/document was settid.
It's also work with all other some things.

So then bind ready (auto IReadyToLocalizationBuild interface) you can use .LocalyzeAsync() to it, or store for now and localize another way. For example:
```csharp
var localizationBuildWithFallback = Nanization.Bind()
                .WithKey("TitleMenu.Title")
                .WithDocument("MyDocument")
                .SetFallback("Title");

// Your custom code here...

tmpText.text = localizationBuildWithFallback.LocalizeAsync();

// Also works fine.
tmpText.text = await Nanization.LocalizeAsync(localizationBuildWithFallback));
```

Bind mode also work fine for selfSourceString:
```csharp
"DefaultUI.ControlPanel.Title".Bind() // Will auto .SetKey("ControlPanel.Title") so IDE will not showing you .SetKey() as possible method
  .AsSelfPath(); // Will also set Document = "DefaultUI", and Key = "ControlPanel.Title".
```

Or another ways:
```csharp
tmpText = await "ControlPanel.Title".Bind()
          .WithDocument("DefaultUI")
          .LocalizeAsync(); // Will find localization with Document = "DefaultUI" Key = "ControlPanel.Title"
```

By the way you can also use "Inline chaining":
```csharp
Nanization.Bind().WithKey("SomeKey").WithDocument("SomeDoc").SetFallback("SomeFallback");
```

Or use vertical chaining, like example below.

## Notes 📝
- Keep it in mind, unlike holy Naninovel accepts the Key first in localization methods, then the Document (key, document), Nanization always requires the Document first, then the Key (document, key), as a more logical order.
- You can use NanizationService to get ITextManager or ILocalizationManager with own hand, without scary of Engine.Initialized == false
```csharp
  var textManager = await NanizationService.GetTextManagerAsync();
  var localizationManager = await NanizationService.GetLocalizationManagerAsync();
```
