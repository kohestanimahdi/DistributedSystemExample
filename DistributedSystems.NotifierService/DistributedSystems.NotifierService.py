import redis
import json
import requests
import os

bill_url = "https://bill.mrbilit.com/api/Bills/Coupons/FirstTime"
redis_server = ""



def lastMomentFlightRequest(request):
    dataRequest = json.loads(request)
    print(dataRequest)

    response = requests.post(
        bill_url,
        json=dataRequest)
    print(response.status_code)



def loadSettings():
    with open("appsettings.json", "r") as file:
        settingFile = file.read().replace("\n", "")

    global notifier_url
    global auth_token
    global redis_server
    global bill_url
    setting = json.loads(settingFile)

    if "bill" in setting:
        if "url" in setting["bill"]:
            bill_url = setting["bill"]["url"]

    redis_server = os.getenv('REDIS_SERVER_HOST', 'localhost')

    

if __name__ == "__main__":
    loadSettings()

    r = redis.Redis(host=redis_server, port=6379, db=0, encoding="utf-8")

    while True:
        sub = r.pubsub()
        sub.subscribe("LastMomentFlightRequest")
        for message in sub.listen():
            if message["type"] != "message":
                continue
            print(message)
            lastMomentFlightRequest(message["data"])
