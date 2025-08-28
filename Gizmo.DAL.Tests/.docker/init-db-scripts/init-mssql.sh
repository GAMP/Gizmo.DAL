#!/bin/bash

# Start SQL Server in the background
/opt/mssql/bin/sqlservr &

# Wait for SQL Server to be ready
sleep 10

# Check if Gizmo database exists
DB_EXISTS=$(/opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "${DB_PASSWORD}" -C -Q "SELECT COUNT(*) FROM sys.databases WHERE name = '${DB_NAME}'" -h -1 2>/dev/null | head -1 | tr -d ' ')

if [ "$DB_EXISTS" -eq 1 ]; then
    /opt/mssql-tools18/bin/sqlcmd \
        -S localhost \
        -U sa \
        -P "${DB_PASSWORD}" \
        -C \
        -Q "USE [master]; DROP DATABASE ${DB_NAME};"
fi

/opt/mssql-tools18/bin/sqlcmd \
    -S localhost \
    -U sa \
    -P "${DB_PASSWORD}" \
    -C \
    -Q "  USE [master];
            RESTORE DATABASE ${DB_NAME} \
            FROM DISK = '/var/opt/mssql/backups/${BACKUP_FILE}' \
            WITH MOVE '${DB_NAME}' TO '/var/opt/mssql/data/${DB_NAME}.mdf', \
            MOVE '${DB_NAME}_log' TO '/var/opt/mssql/data/${DB_NAME}_log.ldf';"

if [ $? -ne 0 ]; then
    echo "MSSQL database restore failed"
    exit 1
fi

# Wait for SQL Server process
wait
