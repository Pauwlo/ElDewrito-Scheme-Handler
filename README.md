# ElDewrito Scheme Handler

This is a small utility that registers a new `eldewrito://` scheme on Windows.

The goal is to run ElDewrito and automatically join a specified server, by clicking on a URI like `eldewrito://allsafe.pauwlo.fr:11720`.

## Installation

Download the [latest release](https://github.com/Pauwlo/ElDewrito-Scheme-Handler/releases/latest).

Extract the ZIP content somewhere, and run `Setup.msi`.

**You're all set!** There is no success message, you can now click on `eldewrito://` links like the ones on [PlebBrowser](http://eldewrito.pauwlo.fr/). :)

## How it works

Registering a new URI scheme on Windows is pretty straightforward. You just need to add a couple registry keys under `HKEY_CLASSES_ROOT`, like so:

```reg
Windows Registry Editor Version 5.00

[HKEY_CLASSES_ROOT\eldewrito]
"URL Protocol"=""

[HKEY_CLASSES_ROOT\eldewrito\shell\open\command]
@="\"C:\\Path\\To\\Program.exe\" \"%1\""
```

This way, you tell Windows to run `C:\Path\To\Program.exe` with the specified resource as a command-line argument, when you click on a URI that begins with `eldewrito://...`.

ElDewrito supports joining a server when the game starts with the `-connect` argument. The problem is, it expects a server hostname or IP in the form of `hostname[:port]`, but Windows will call the handler application with the full URI as an argument. This may cause ElDewrito to crash because it doesn't expect to receive the following parameter : `eldorado.exe -connect eldewrito://allsafe.pauwlo.fr:11720/`

To circumvent this, this repository contains a small C# application that not only parses and fixes the server hostname for you, but also registers itself in registry and helps you select your ElDewrito installation path.

This means you can get click-to-join support for ElDewrito in a couple clicks!
