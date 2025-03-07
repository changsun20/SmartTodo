# SmartTodo

![Build Status](https://github.com/changsun20/SmartTodo/actions/workflows/release.yml/badge.svg)
![Test Badge](https://github.com/changsun20/SmartTodo/actions/workflows/pr-test.yml/badge.svg)
![Release](https://img.shields.io/github/v/release/changsun20/SmartTodo)
![License](https://img.shields.io/github/license/changsun20/SmartTodo)

A lightweight, efficient command-line todo list application written in C#. SmartTodo helps you manage your tasks effectively with a simple and intuitive interface.

## Features

- **Command-line focused**: Manage your todos without leaving the terminal
- **Easy to use**: Simple commands for adding, removing, and updating tasks
- **Cross-platform**: Runs on Windows, macOS, and Linux
- **Efficient**: AOT-compiled for optimal performance
- **No dependencies**: Stand-alone binary with no need to install .NET SDK
- **Future web integration**: Planning to add web interface in upcoming releases

## Installation

### Download the binary

Pre-compiled binaries are available for all major platforms. No .NET SDK installation required!

1. Go to the [releases page](https://github.com/changsun20/SmartTodo/releases)
2. Download the appropriate binary for your operating system
3. Make the file executable (on Unix-based systems): `chmod +x SmartTodo`

### Running from source

If you prefer to build from source:

```bash
git clone https://github.com/changsun20/SmartTodo.git
cd SmartTodo
dotnet build
```

## Usage

```bash
# Start the CLI app
SmartTodo

# Add a task
> add "Complete project documentation"

# List all tasks
> list

# Mark a task as completed
> complete 1

# Remove a task
> delete 2
```

## Roadmap

- [ ] JSON data persistence
- [ ] Colorful output
- [ ] Task prioritization
- [ ] Due dates
- [ ] Task categories and tags
- [ ] Command autofill
- [ ] Web interface integration

## Development

SmartTodo uses GitHub Actions for CI/CD:
- Automated testing on all commits
- Automated release building
- Cross-platform binary generation

Check the [actions page](https://github.com/changsun20/SmartTodo/actions) for build status.

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## License

Distributed under the MIT License. See `LICENSE` for more information.