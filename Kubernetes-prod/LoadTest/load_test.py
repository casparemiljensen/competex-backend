from locust import HttpUser, task, between

class NginxUser(HttpUser):
    wait_time = between(1, 2)

    @task
    def load_nginx(self):
        self.client.get("/")