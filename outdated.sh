
#!/bin/bash

# get list of projects in solution
projects=($(dotnet sln list))

# loop through each project and list outdated packages
for project in "${projects[@]}"
do
  echo "=== $project ==="
  outdated=$(dotnet list "$project" package --outdated | awk '/>/{print $2}')
  if [ -z "$outdated" ]; then
    echo "No outdated packages found."
  else
    echo "$outdated"
  fi
done

