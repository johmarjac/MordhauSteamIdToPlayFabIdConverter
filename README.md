# Mordhau SteamId to PlayFabId Converter

## Description
This tool queries the PlayFabApi of the Mordhau Title to convert a list of SteamIDs to PlayFabIDs.

## Facts
For educational reasons I spent quite a bit of more time than necessary to provide a Plugin System for
Input and Output Format Providers.
By default there are currently the following Providers:

### Input Providers
* Line Input Provider (Reads the input file line by line each containing a Steam Id)
* Json Input Provider (Reads the input file and parses a String Array)

### Output Providers
* Json Output Provider (Outputs the Conversion Result originally like the PlayFabApi returns it)

You can develop a simple plugin very fast to suite your needs if you have lots of SteamIds in a different format.
To do so, create a .NET Core Class-Library Project and reference the MordhauTools.Shared Library.

An InputProvider Class inherits from the `IInputConversionProvider` interface.
An OutputProvider Class inherits from the `IOutputConversionProvider` interface.

## How to use
Depending on the selected Input- and Output Provider, you simply select the Input File (containing your SteamIds)
and your Output File where to save the result to. However this depends on the Plugin you use. The tool was initially
designed to have an Input and an Output file. If you however have different approaches to get a list of SteamIds you could always ignore the Input File and get your SteamIds differently in your own Input Provider. This is totally up to you.

If you have questions you can DM me on Discord: `johmarjac#3365`

## Example
Line Input Provider - Input

`InputSteamIds.txt`
```
76561198863411635
76561198863411635
76561198863411635
76561198863411635
76561198863411635
```
(Each SteamId in a new row)

Json Output Provider - Output
```json
[
  {
    "PlayFabId": "B9D0E9C7B7ABB094",
    "SteamStringId": "76561198863411635"
  },
  {
    "PlayFabId": "B9D0E9C7B7ABB094",
    "SteamStringId": "76561198863411635"
  },
  {
    "PlayFabId": "B9D0E9C7B7ABB094",
    "SteamStringId": "76561198863411635"
  },
  {
    "PlayFabId": "B9D0E9C7B7ABB094",
    "SteamStringId": "76561198863411635"
  },
  {
    "PlayFabId": "B9D0E9C7B7ABB094",
    "SteamStringId": "76561198863411635"
  }
]
```