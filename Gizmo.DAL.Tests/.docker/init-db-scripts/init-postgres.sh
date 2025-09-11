#!/bin/bash

# Wait for PostgreSQL to be ready
sleep 10

psql -U "${POSTGRES_USER}" -d postgres -c "DROP DATABASE IF EXISTS ${DB_NAME}; CREATE DATABASE ${DB_NAME};"
pg_restore -U "${POSTGRES_USER}" -d "${DB_NAME}" --clean --if-exists --verbose "${BACKUP_PATH}/${BACKUP_FILE}"

if [ $? -ne 0 ]; then
    echo "PostgreSQL database restore failed"
    exit 1
fi
