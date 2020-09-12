# coronaampel-bot

Receive current CoronaAmpel values for your bezirk.

## Usage
```sh
dotnet run Telegram:AccessToken="yourtelegramaccesstoken" Telegram:ChatId="yourtelegramchatid" Message:Format="Die Ampel in Wien ist auf {0}" Commune:Id="123"
```

## Data provided by
[Austrian government (Corona Ampel)](https://www.data.gv.at/katalog/dataset/52abfc2b-031c-4875-b838-653abbfccf4e)
