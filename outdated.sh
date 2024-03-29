#!/bin/bash

# Get list of projects in solution but ignore the first two lines
# which are 'Project(s)' and '-------'
projects=($(dotnet sln list | awk 'NR>2'))

# Loop through each project and list outdated packages
for project in "${projects[@]}"
do
  echo "=== $project ==="
  outdated=$(dotnet list "$project" package --outdated | awk '/>/{print $2}')
  if [ -z "$outdated" ]; then
    echo "No outdated packages found."
  else
    # Split the list of outdated packages into an array
    readarray -t packages <<<"$outdated"

    # Loop through each outdated package and update it
    for package in "${packages[@]}"
    do
      echo "Updating $package"
      output=$(dotnet add "$project" package "$package" 2>&1)
      if [ $? -eq 0 ]; then
        echo "Successfully updated $package"
      else
        echo "Failed to update $package: $output"
      fi
    done
  fi
done
