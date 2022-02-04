import redis
import json
import requests
import os

auth_token = ""
notifier_url = ""
redis_server = ""


def sendSmsMessage(message):
    dataMessage = json.loads(message)
    text = ""
    mobile = ""
    if "mobile" in dataMessage:
        mobile = dataMessage["mobile"]
    elif "Mobile" in dataMessage:
        mobile = dataMessage["Mobile"]
    else:
        raise ValueError("Message should contain Mobile")

    if "Message" in dataMessage:
        text = dataMessage["Message"]
    elif "message" in dataMessage:
        text = dataMessage["message"]
    else:
        raise ValueError("Message should contain Message")

    print(mobile)
    print(text)
    response = requests.post(
        notifier_url,
        json={"phoneNumber": mobile, "message": text},
        headers={"Authorization": "Bearer " + auth_token},
    )
    print(response)


def loadSettings():
    with open("appsettings.json", "r") as file:
        settingFile = file.read().replace("\n", "")

    global notifier_url
    global auth_token
    global redis_server
    setting = json.loads(settingFile)
    if "notifier" in setting:
        if "url" in setting["notifier"]:
            notifier_url = setting["notifier"]["url"]
        if "token" in setting["notifier"]:
            auth_token = setting["notifier"]["token"]

    redis_server = os.getenv('REDIS_SERVER_HOST', 'localhost')

    

if __name__ == "__main__":
    loadSettings()

    r = redis.Redis(host=redis_server, port=6379, db=0, encoding="utf-8")

    while True:
        sub = r.pubsub()
        sub.subscribe("SendSMSMessage")
        for message in sub.listen():
            if message["type"] != "message":
                continue
            print(message)
            sendSmsMessage(message["data"])
