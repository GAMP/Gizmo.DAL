#!/bin/bash

# Start SQL Server in the background
/opt/mssql/bin/sqlservr &

# Wait for SQL Server to be ready
sleep 10

# Disable remote admin connections to prevent DAC conflicts
/opt/mssql-tools18/bin/sqlcmd \
    -S localhost \
    -U sa \
    -P "${MSSQL_SA_PASSWORD}" \
    -C \
    -Q "EXEC sp_configure 'remote admin connections', 0; RECONFIGURE;"

# Restore the database
/opt/mssql-tools18/bin/sqlcmd \
    -S localhost \
    -U sa \
    -P "${MSSQL_SA_PASSWORD}" \
    -C \
    -Q "  USE [master];
            DROP DATABASE IF EXISTS ${DB_NAME};
            RESTORE DATABASE ${DB_NAME} \
            FROM DISK = '${BACKUP_PATH}/${BACKUP_FILE}' \
            WITH MOVE '${DB_NAME}' TO '/var/opt/mssql/data/${DB_NAME}.mdf', \
            MOVE '${DB_NAME}_log' TO '/var/opt/mssql/data/${DB_NAME}_log.ldf';"

if [ $? -ne 0 ]; then
    echo "MSSQL database restore failed"
    exit 1
fi

# Wait for SQL Server process
wait
