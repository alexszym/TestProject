FROM mongo

COPY seed.json /seed.json
CMD mongoimport --host mongodb --db TestDatabase --collection Products --upsert --type json --file /seed.json --jsonArray