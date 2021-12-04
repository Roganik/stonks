#bin/sh

echo "\n== Downloading swagger.json from running dotnet app =="
rm swagger.json
curl https://localhost:5001/swagger/v1/swagger.json > swagger.json

echo "Successfully downloaded a swagger.json"
echo "\n== Generating typescript models =="

# generate models
# install openapi-generator from here: https://github.com/OpenAPITools/openapi-generator
openapi-generator generate \
-i ./swagger.json \
-g typescript \
-o ./src/api/generated

echo "Done"
echo "\n== Removing swagger.json =="
# remove swagger file
rm swagger.json