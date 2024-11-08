#!/bin/bash

cleanup() {
    echo "Stopping the server..."
    kill "$app_pid" 2>/dev/null
    kill "$firefox_pid" 2>/dev/null
    echo "Server stopped."
    exit 0
}

# Trap SIGINT (Ctrl+C) and SIGTERM (kill command) and call cleanup
trap cleanup SIGINT SIGTERM

# Start the .NET application and capture the output
dotnet run --project ./competex-backend > app_output.log 2>&1 &

# Store the process ID of the application
app_pid=$!

# Wait for a specific log output that indicates the application has started
echo "Waiting for the application to start..."
started=false

# Loop to check the log file for the startup message
while ! $started; do
    if grep -q "Now listening on" app_output.log; then
        started=true
    else
        sleep 1
    fi
done

# Open the Swagger UI in the default browser
swagger_url="http://localhost:5228/swagger/index.html"
firefox --new-window "$swagger_url" &
firefox_pid=$!

echo "Firefox PID: $firefox_pid."

echo "Swagger UI opened at $swagger_url."

# Wait for the application to exit
wait $firefox_pid

# Call cleanup when Firefox window closes
cleanup
exit 0