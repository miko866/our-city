echo "Dockerize application and deploy to Container Registry"
TAG_NAME=$CI_COMMIT_REF_NAME
if [ "$CI_COMMIT_REF_NAME" == "main" ]; then
  TAG_NAME="latest"
fi

echo "{\"auths\":{\"$CI_REGISTRY\":{\"username\":\"$CI_REGISTRY_USER\",\"password\":\"$CI_REGISTRY_PASSWORD\"}}}" > /kaniko/.docker/config.json || exit_error "WRITING JSON"
/kaniko/executor --dockerfile Dockerfile --destination "$CI_REGISTRY_IMAGE":$TAG_NAME --context "$PWD"

echo "Dockerize and put to Container Registry successful"
