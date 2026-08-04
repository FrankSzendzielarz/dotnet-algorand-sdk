if [ "$ver" == "" ]; then
ver=4.7.4
fi

echo "docker build -t \"scholtz2/dotnet-avm-generated-client:$ver-main\" -f client-generator/Dockerfile ./"

#echo "{\"v\":\"$ver\"}" > "../soldier/version.json"

#echo "version.json:"
#cat ../soldier/version.json

docker build -t "scholtz2/dotnet-avm-generated-client:$ver-main" -f client-generator/Dockerfile  ./ || error_code=$?
if [ "$error_code" != "" ]; then
echo "$error_code";
    echo "failed to build";
	exit 1;
fi

docker push "scholtz2/dotnet-avm-generated-client:$ver-main"  || error_code=$?
if [ "$error_code" != "" ]; then
echo "$error_code";
    echo "failed to push";
	exit 1;
fi

docker tag "scholtz2/dotnet-avm-generated-client:$ver-main" "scholtz2/dotnet-avm-generated-client:latest"  || error_code=$?
if [ "$error_code" != "" ]; then
echo "$error_code";
    echo "failed to push";
	exit 1;
fi

docker push "scholtz2/dotnet-avm-generated-client:latest" || error_code=$?
if [ "$error_code" != "" ]; then
echo "$error_code";
    echo "failed to push";
	exit 1;
fi

echo "Image: scholtz2/dotnet-avm-generated-client:latest"
echo "Image: scholtz2/dotnet-avm-generated-client:$ver-main"