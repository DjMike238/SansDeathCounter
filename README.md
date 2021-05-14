# Sans Death Counter

Sans Death Counter is a simple utility aimed at streamers who play Undertale.

It reads the amount of deaths versus Sans from the game's savefile (located in `%localappdata%\UNDERTALE\undertale.ini`) and writes it on a file called `deaths.txt`, with the prefix `Deaths:`. The file is updated as soon as the game writes new data on `undertale.ini`.

## Usage

1. Download the latest release from the [Releases](https://github.com/DjMike238/SansDeathCounter/releases) section.
2. Open `SansDeathCounter.exe`: a `deaths.txt` file will be generated.
3. On your streaming software (OBS/Streamlabs) add a Text source that reads from `deaths.txt`.

That's it! Just remember to **keep the program open while streaming**, otherwise the counter won't be updated.

## To-do list

- [ ] Ability to change the default prefix `Deaths:`
