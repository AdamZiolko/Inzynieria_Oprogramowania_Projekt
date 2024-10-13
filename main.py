import os

def rename_files(directory, max_depth):
    for root, _, files in os.walk(directory):
        # Calculate the current depth
        depth = root[len(directory):].count(os.sep)
        print(f"Current directory: {root}, Depth: {depth}")  # Debug statement
        if depth < max_depth:
            for file in files:
                print(f"Checking file: {file}")  # Debug statement
                if "AuthSystem" in file:
                    new_file = file.replace("AuthSystem", "Project.Bookworm")
                    print(f"Renaming {file} to {new_file}")  # Debug statement
                    os.rename(os.path.join(root, file), os.path.join(root, new_file))
                    print(f"Renamed {file} to {new_file}")  # Print result

if __name__ == "__main__":
    directory = "."
    max_depth = 100
    rename_files(directory, max_depth)