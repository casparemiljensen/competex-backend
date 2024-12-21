from locust import HttpUser, task, between
import socket
import json
import logging


# Configure logging
logging.basicConfig(level=logging.INFO)
logger = logging.getLogger(__name__)


class NginxUser(HttpUser):
    wait_time = between(1, 2)
    host = "http://backend.localhost"  # Replace with your actual host

    @task
    def load_nginx(self):
        response = self.client.post(
            "/api/members/search", 
            data=json.dumps(
                {
                 "FirstName": "Janni"
                }
            ), 
            headers=
                {
                    "Content-Type": "application/json"
                }
        )

      # Print response details
                # Log response details
        #logger.info("Status Code: %s", response.status_code)
        #logger.info("Response Text: %s", response.text)
        #if response.status_code == 200:
        #    logger.info("Response JSON: %s", response.json())
        # self.client.get("/api/members/")
        #self.client.get("/")